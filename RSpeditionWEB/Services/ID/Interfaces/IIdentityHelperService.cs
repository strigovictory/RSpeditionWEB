using RSpeditionWEB.Models.ID;

namespace RSpeditionWEB.Services.ID.Interfaces;

public interface IIdentityHelperService
{
    Task<WebUser> Authenticate(LoginRequest request);

    Task<WebUser> GetIdentityUser();
}
