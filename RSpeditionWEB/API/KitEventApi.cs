using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API;

public class KitEventApi : ApiPointsBase
{
    public KitEventApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.eventtypes;


    public async Task<List<KitEventTypeView>> GetKitEventTypes(CancellationToken token)
        => (await http?.SendRequestAsync<List<KitEventTypeView>>(
            GetApiPoint(),
            HttpMethod.Get,
            token)) ?? null;
}
