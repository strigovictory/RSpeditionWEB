using RSpeditionWEB.Models.ID;

namespace RSpeditionWEB.Helpers.Auth
{
    public static class ClaimsPrincipalWebUserMapper
    {
        public static WebUser MappingClaimsPrincipalToWebUser(this ClaimsPrincipal claims)
        {
            WebUser user = new();

            user.UserName = claims.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
            user.Email = claims.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email)?.Value;
            user.IsAuthenticated = bool.Parse(claims.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.UserData)?.Value);
            user.IsAdmin = bool.Parse(claims.Claims.LastOrDefault(_ => _.Type == ClaimTypes.UserData)?.Value);
            user.Roles = claims.Claims.Where(_ => _.Type == ClaimTypes.Role)?.ToList()?.Select(_ => _.Value)?.ToList() ?? new();

            return user;
        }

        public static ClaimsPrincipal MappingWebUserToClaimsPrincipal(this WebUser user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("AppUserAuth", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName ?? string.Empty, ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? string.Empty, ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimTypes.UserData, user.IsAuthenticated.ToString(), ClaimValueTypes.Boolean));
            claim.AddClaim(new Claim(ClaimTypes.UserData, user.IsAdmin.ToString(), ClaimValueTypes.String));
            user.Roles?.ToList()?.ForEach(_ => claim.AddClaim(new Claim(ClaimTypes.Role, _, ClaimValueTypes.String)));

            return new ClaimsPrincipal(new List<ClaimsIdentity> { claim });
        }
    }
}
