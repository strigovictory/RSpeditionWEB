using RSpeditionWEB.Enums.Roles;

namespace RSpeditionWEB.Components.ComponentsBase;

public class AuthenticationStateBaseClass : ComponentBase
{
    [CascadingParameter]
    protected Task<AuthenticationState> AuthenticationState { get; set; }

    // Имя пользователя, если он авторизован
    private string user;
    protected string User
    {
        get
        {
            Func<Task<string>> f = async () => await this.GetUserName();
            return f().Result ?? string.Empty;
        }
        set => this.user = value ?? string.Empty;
    }

    protected virtual FuelRoles MinimumFuelRole { get; set; } = FuelRoles.Spedition_User;

    protected bool IsUserHasMinimumFuelRole => IsUserHasFuelRoleInHierarchy(MinimumFuelRole);
        
    protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

    /// <summary>
    /// Метод для получения имени пользователя
    /// </summary>
    /// <returns></returns>
    private async Task<string> GetUserName() => (await this.AuthenticationState)?.User?.Identity?.Name ?? string.Empty;

    /// <summary>
    /// Метод для определения, авторизован пользователь или нет
    /// </summary>
    /// <returns></returns>
    protected async Task<bool> IsUserAuthenticated() => (await this.AuthenticationState)?.User?.Identity?.IsAuthenticated ?? false;

		protected bool IsUserAuth
		{
			get 
        {
				Func<Task<bool>> f = async () => await this.IsUserAuthenticated();
				return f().Result;
			}
		}

		protected bool IsUserIsInRole(RolesNames roleName)
        => (this.AuthenticationState.Result)?.User?.IsInRole(Enum.GetName(roleName)) ?? false;

    protected async Task<bool> IsUserIsInRoleAsync(RolesNames roleName)
       => (await this.AuthenticationState)?.User?.IsInRole(Enum.GetName(roleName)) ?? false;

    protected bool IsUserIsAuthenticated()
       => (this.AuthenticationState.Result)?.User?.Identity?.IsAuthenticated ?? false;

    //[
    //    {
    //        "id": "9e30b958-f7de-4fae-8c7c-5fccb060af45",
    //        "name": "Administrator",
    //        "normalizedName": "ADMINISTRATOR",
    //        "concurrencyStamp": "b77e3fd8-cba2-4339-8e2c-dd364255bff6"
    //    },
    //    {
    //    "id": "ef2877a9-9116-4581-9776-187140a986fc",
    //        "name": "Spedition_User",
    //        "normalizedName": "SPEDITION_USER",
    //        "concurrencyStamp": "e390d4da-24fa-4c54-902b-9d7040e525b9"
    //    },
    //    {
    //    "id": "89a3f555-bbb0-4d13-bf1e-4456de577c9d",
    //        "name": "Super_Administrator",
    //        "normalizedName": "SUPER_ADMINISTRATOR",
    //        "concurrencyStamp": "89160184-197d-445b-89b4-811e0b3afaee"
    //    },
    //    {
    //    "id": "5ff61ddb-09af-4f57-90cb-bbac79bab0b3",
    //        "name": "Fuel_Accountant",
    //        "normalizedName": "FUEL_ACCOUNTANT",
    //        "concurrencyStamp": null
    //    },
    //    {
    //    "id": "3af9ab1a-0ac1-43ee-b747-2aabe5dd8a63",
    //        "name": "Fuel_Provisioner",
    //        "normalizedName": "FUEL_PROVISIONER",
    //        "concurrencyStamp": null
    //    },
    //]

    private static Dictionary<FuelRoles, List<FuelRoles>> RolesHierarchy =>
        new Dictionary<FuelRoles, List<FuelRoles>>
        {
            { 
                FuelRoles.Super_Administrator,
                new List<FuelRoles>
                {
                    FuelRoles.Administrator,
                    FuelRoles.Fuel_Provisioner,
                    FuelRoles.Fuel_Accountant,
                    FuelRoles.Spedition_User,
                }
            },
            { 
                FuelRoles.Administrator,
                new List<FuelRoles>
                {
                    FuelRoles.Fuel_Provisioner,
                    FuelRoles.Fuel_Accountant,
                    FuelRoles.Spedition_User,
                }
            },
            { 
                FuelRoles.Fuel_Provisioner,
                new List<FuelRoles>
                {
                    FuelRoles.Fuel_Accountant,
                    FuelRoles.Spedition_User,
                }
            },
            { 
                FuelRoles.Fuel_Accountant,
                new List<FuelRoles>
                {
                    FuelRoles.Spedition_User,
                }
            },
            {
                FuelRoles.Spedition_User,
                new List<FuelRoles>
                {
                }
            },
        };

    private bool IsUserIsInFuelRole(FuelRoles roleName)
        => AuthenticationState.Result?.User?.IsInRole(Enum.GetName(roleName)) ?? false;

    protected bool IsUserHasFuelRoleInHierarchy(FuelRoles roleName)
    {
        var isInRole = IsUserIsInFuelRole(roleName);

        if (!isInRole)
        {
            var userRoles = AuthenticationState.Result?.User?.MappingClaimsPrincipalToWebUser()?.Roles?.ToList() ?? new();
            foreach (var role in userRoles)
            {
                if(isInRole) return true;
                if(Enum.TryParse(typeof(FuelRoles), role, out var fuelRoleToSearch) && fuelRoleToSearch is FuelRoles fuelRoleToSearchCasted)
                {
                    if (RolesHierarchy.TryGetValue(fuelRoleToSearchCasted, out var availableRoles))
                        isInRole = availableRoles.Contains(roleName);
                }
            }
        }

        return isInRole;
    }
}