using RestSharp;
using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.ID;
using RSpeditionWEB.Services.ID.Helpers;
using RSpeditionWEB.Services.ID.Interfaces;
using System.Net;

namespace RSpeditionWEB.Services.ID;

public class IdentityHelperService : IIdentityHelperService
{
    #region Variables
    private readonly Uri identityServerUrl;
    private readonly RestClient applicationClient;
    private CookieContainer cookies = new();
    private List<CookieLocal> cookieLocals = new();
    private readonly AppAuthenticationStateProvider stateProvider;

    #endregion

    public IdentityHelperService(AppAuthenticationStateProvider stateProvider, IOptions<IdentityServerConfigurations> options)
    {
        this.stateProvider = stateProvider;
        identityServerUrl = new(options.Value.BaseUrl);
        applicationClient = new(identityServerUrl);
    }

    public async Task<WebUser> Authenticate(LoginRequest incomingRequest)
    {
        var outgoingRequest = new RestRequest(identityServerUrl + "/login", Method.Post);
        outgoingRequest.AddHeader("Content-Type", "application/json");
        outgoingRequest.AddBody(incomingRequest);

        var response = await QueryHandlerExtension.ExecuteRequestQueryPost(applicationClient, outgoingRequest);

        if (response == null || (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest))
        {
            return null;
        }
        else if (!response.IsSuccessStatusCode)
        {
            return new();
        }
        
        foreach (var header in response.Headers)
        {
            if (header.Name == "Set-Cookie")
            {
                cookies.SetCookies(identityServerUrl, header.Value.ToString());
            }
        }

        var cookieValues = cookies.GetCookies(identityServerUrl).Cast<Cookie>();
        foreach (var cookie in cookieValues)
        {
            if (!string.IsNullOrEmpty(cookie.Name))
            {
                cookieLocals.Add(new CookieLocal
                {
                    Name = cookie.Name,
                    Value = cookie.Value,
                    Path = cookie.Path,
                    Domain = cookie.Domain,
                });
            }
        }

        return await GetIdentityUser();
    }

    public async Task<WebUser> GetIdentityUser()
    {
        var outgoingRequest = new RestRequest(identityServerUrl + "/currentuserinfo", Method.Get);

        foreach (var cookie in cookieLocals ?? new())
        {
            outgoingRequest.AddCookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
        }

        var user = await QueryHandlerExtension.ExecuteRequestQueryGet<WebUser>(applicationClient, outgoingRequest);

        await this.stateProvider.SetUserInProtectSessionStorage(user);

        return user;
    }
}
