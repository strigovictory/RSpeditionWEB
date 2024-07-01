using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API;

public class FuelTypeApi : ApiPointsBase
{
    #region
    public FuelTypeApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fueltypes;
    #endregion

    /// <summary>
    /// Метод для получения коллекции разновидностей услуг на заправочных станциях
    /// </summary>
    /// <returns>Коллекция разновидностей услуг на заправочных станциях</returns>
    public async Task<List<FuelTypeResponse>> GetFuelTypes(CancellationToken token)
        => (await http?.SendRequestAsync<List<FuelTypeResponse>>(
            GetApiPoint(),
            HttpMethod.Get,
            token)) ?? new();
}
