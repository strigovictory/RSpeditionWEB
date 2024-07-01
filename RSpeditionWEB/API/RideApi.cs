using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API;

public class RideApi : ApiPointsBase
{
    #region
    public RideApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.rides;
    #endregion

    public async Task<List<RideView>> GetRides(CancellationToken token, int pageSize = 10000)
    {
        List<RideView> res = new();

        // 1 - Запрос кол-ва всего в БД
        var uri = GetApiPoint(nameof(GetRides));

        var itemsCount = await http?.SendRequestAsync<int>(
            uri,
            HttpMethod.Get,
            token);
        var resDevide = (int)(itemsCount / pageSize);

        // 2 - Запрос с пагинацией 
        var pagesNum = itemsCount % pageSize > 0 ? resDevide + 1 : resDevide;

        var pageCurrent = 0;
        var skipCurrent = 0;

        while (pageCurrent != pagesNum)
        {
            var queryByPage = $"{uri}?skip={skipCurrent}&top={pageSize}";
            var item = await http?.SendRequestAsync<List<RideView>>(
                queryByPage,
                HttpMethod.Get,
                token);
            res.AddRange(item);
            pageCurrent++;
            skipCurrent += pageSize;
        }

        return res;
    }

    public async Task<List<RideView>> GetRides(CancellationToken token)
        => (await http?.SendRequestAsync<List<RideView>>(
            GetApiPoint(nameof(GetRides)),
            HttpMethod.Get,
            token)) ?? new();
}
