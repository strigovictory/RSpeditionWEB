using RSpeditionWEB.Models.ID;

namespace RSpeditionWEB.Services
{
    // Класс, который переопределяет поставщика состояния аутентификации пользователя в приложении
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage store;

        public AppAuthenticationStateProvider(ProtectedSessionStorage store)
        {
            this.store = store;
        }

        private ClaimsPrincipal ClaimsPrincipalWebUser(WebUser user) => user.MappingWebUserToClaimsPrincipal();

        public bool IsWebUserAuthenticated
        {
            get
            {
                Func<Task<bool>> f = async () => (await this.GetAuthenticationStateAsync())?.User?.Identity?.IsAuthenticated ?? false;
                return f().Result;
            }
        }

        public async Task<WebUser> ReadWebUser() => (await this.store.GetAsync<WebUser>("user").AsTask()).Value;

        public async Task SetUserInProtectSessionStorage(WebUser user) 
        {
            await this.store.SetAsync("user", user);
            this.NotifyAboutState();
        }

        public async Task ResetUserFromProtectSessionStorage()
        {
            await this.store.DeleteAsync("user");
            this.NotifyAboutState();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Получить пользователя из защищенного хранилища сессий
            var webUser = await this.ReadWebUser();

            // Если пользователь успешно считан - проинициализировать клеймы
            if (webUser != null && !string.IsNullOrEmpty(webUser.UserName))
                return new AuthenticationState(this.ClaimsPrincipalWebUser(webUser));
            else
                return new AuthenticationState(new(new List<ClaimsIdentity>()));
        }

        /// <summary>
        /// Известить приложение об изменении состояния аутентификации пользователя  в приложении -
        // этот метод запускает событие event AuthenticationStateChangedHandler AuthenticationStateChanged
        /// </summary>
        private void NotifyAboutState() => this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
    }
}
