using RSpeditionWEB.Configs;
using System.Runtime;

namespace RSpeditionWEB.Services;

public sealed class HttpService : IHttpService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ProtectedSessionStorage store;
    private readonly GateWayConfigurations configs;

    public HttpService(
        IHttpClientFactory httpClientFactory,
        ProtectedSessionStorage store,
        IOptions<GateWayConfigurations> options)
    {
        this.httpClientFactory = httpClientFactory;
        this.store = store;
        this.configs = options.Value;
    }

    public string NotifyMessage { get; set; } = string.Empty;

    private HttpClient HttpClient =>
        httpClientFactory?.CreateClient("fuelapiclient")
        ?? new HttpClient();

    public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(
        string url,
        HttpMethod httpMethod,
        TRequest content,
        CancellationToken token = default)
    {
        var request = url.GetRequestMessage(httpMethod, content, configs.Key);
        var response = await HttpClient?.SendRequest(request, token);
        if (response == null || !response.IsSuccessStatusCode)
        {
            NotifyMessage = response?.ReasonPhrase ?? "Операция закончилась с ошибкой !";
            return default;
        }

        return await response.DeserializeTResponse<TResponse>();
    }

    public async Task<TResponse> SendRequestAsync<TResponse>(
        string url,
        HttpMethod httpMethod,
        CancellationToken token = default)
    {
        var request = url.GetRequestMessage(httpMethod, configs.Key); 
        var response = await HttpClient?.SendRequest(request, token);

        if (response == null || !response.IsSuccessStatusCode)
        {
            NotifyMessage = response?.ReasonPhrase ?? "Операция закончилась с ошибкой !";
            return default;
        }

        return await response.DeserializeTResponse<TResponse>();
    }
}
