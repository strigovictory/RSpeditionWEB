using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API;

public class RideDriverReportApi : ApiPointsBase
{
    #region
    public RideDriverReportApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.ridedriverreports;
    #endregion

    public async Task<List<RideDriverReportShortView>> GetRideDriverReports(int emplId, DateTime date, CancellationToken token)
        => (await http?.SendRequestAsync<List<RideDriverReportShortView>>(
            string.Concat(
                GetApiPoint(nameof(GetRideDriverReports)),
                $"?emplId={emplId}&date={date.ToBinary()}"),
            HttpMethod.Get,
            token: token))
        ??
        new()
        ;

    public async Task<List<RideDriverReportView>> GetRideDriverReports(CancellationToken token)
        => (await http?.SendRequestAsync<List<RideDriverReportView>>(
            GetApiPoint(nameof(GetRideDriverReports)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<RideDriverReportShortView>> GetRideDriverReportsShort(CancellationToken token)
        => (await http?.SendRequestAsync<List<RideDriverReportShortView>>(
            GetApiPoint(nameof(GetRideDriverReportsShort)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<RideDriverReportShortView>> GetRideDriverReports(CancellationToken token, int pageSize = 10000)
    {
        List<RideDriverReportShortView> res = new();

        // 1 - Запрос кол-ва всего в БД
        var uri = GetApiPoint(nameof(GetRideDriverReports));

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
            var item = await http?.SendRequestAsync<List<RideDriverReportShortView>>(
                queryByPage,
                HttpMethod.Get, 
                token);
            res.AddRange(item);
            pageCurrent++;
            skipCurrent += pageSize;
        }

        return res;
    }
}
