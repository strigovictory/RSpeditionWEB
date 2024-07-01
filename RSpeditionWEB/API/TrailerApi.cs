using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API;

public class TrailerApi : ApiPointsBase
{
    public TrailerApi(
        IOptions<GateWayConfigurations> options,
        IHttpService httpService)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.trailers;

    #region Get
    public async Task<List<TrailerView>> GetTrailers(CancellationToken token)
        => (await http?.SendRequestAsync<List<TrailerView>>(
            GetApiPoint(),
            HttpMethod.Get,
            token)) ?? new();
    #endregion
}
