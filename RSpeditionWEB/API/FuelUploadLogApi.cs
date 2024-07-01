using RSpeditionWEB.Configs;
using System;

namespace RSpeditionWEB.API;

public class FuelUploadLogApi : ApiPointsBase
{
    public FuelUploadLogApi(IHttpService http, IOptions<GateWayConfigurations> options) : base(http, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fueluploadlogs;

    public async Task<List<FuelTransactionUploadLogResponse>> GetFuelTransactionUploadLogs(
        int year, int? month = null, int? providerId = null, CancellationToken token = default)
    {
        var uriWithQueryParameters = $"{GetApiPoint()}?year={year}&month={month}&providerId={providerId}";

        return await http?.SendRequestAsync<List<FuelTransactionUploadLogResponse>>(
            uriWithQueryParameters,
            HttpMethod.Get,
            token) ?? new();
    }
}
