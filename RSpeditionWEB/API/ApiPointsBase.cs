using RSpeditionWEB.Configs;
using RSpeditionWEB.Services;

namespace RSpeditionWEB.API;

public abstract class ApiPointsBase
{
    private readonly GateWayConfigurations configuration;
    protected readonly IHttpService http;

    public string NotifyMessage => http?.NotifyMessage ?? string.Empty;

    public ApiPointsBase(IHttpService http, IOptions<GateWayConfigurations> options)
    {
        this.configuration = options.Value; 
        this.http = http;
    }

    public enum ControllersNames
    {
        fueltransactions,
        fuelcards,
        fuelcardsevents,
        fuelparser,
        fueltypes,
        fuelproviders,
        fuelprint,
        fueluploadlogs,
        eventtypes,
        trucks,
        trailers,
        currencies,
        countries,
        divisions,
        finance, 
        employees,
        ridedriverreports,
        rides,
        mobile,
        admin,
    }

    protected virtual string HostUrl => configuration.BaseUrl + "/api/v1";

    public abstract ControllersNames ControllerName { get; }

    protected string GetApiPoint(string routeSegment = null) 
        => !string.IsNullOrEmpty(routeSegment) 
        ? $"{HostUrl}/{ControllerName.ToString()}/{routeSegment}"
        : $"{HostUrl}/{ControllerName.ToString()}";
}
