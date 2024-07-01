using RSpeditionWEB.Configs;
using static System.Net.WebRequestMethods;

namespace RSpeditionWEB.API;

public class FuelProviderApi : ApiPointsBase
{
    #region
    public FuelProviderApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fuelproviders;
    #endregion

    /// <summary>
    /// Метод для получения коллекции поставщиков топлива
    /// </summary>
    /// <returns>Коллекция поставщиков топлива</returns>
    public async Task<List<FuelProviderResponse>> GetFuelProviders(CancellationToken token)
        => (await http?.SendRequestAsync<List<FuelProviderResponse>>(
            GetApiPoint(),
            HttpMethod.Get,
            token)) ?? new();
}
