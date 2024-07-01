using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.RequestModels;

namespace RSpeditionWEB.API;

public class FuelCardsEventsApi : ApiPointsBase
{
    #region
    public FuelCardsEventsApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fuelcardsevents;
    #endregion

    #region GET
    public async Task<List<FuelCardsEventResponse>> GetCardsEvents(int cardId, CancellationToken token)
        => (await http?.SendRequestAsync<List<FuelCardsEventResponse>>(
            GetApiPoint($"{cardId}"),
            HttpMethod.Get,
            token)) ?? null;

    public async Task<FuelCardsEventResponse> GetCardsEventPrevious(int eventId, CancellationToken token)
        => (await http?.SendRequestAsync<FuelCardsEventResponse>(
            GetApiPoint($"previous/{eventId}"),
            HttpMethod.Get,
            token: token)) ?? null;

    public async Task<FuelCardsEventResponse> GeCardsEventNext(int eventId, CancellationToken token)
        => (await http?.SendRequestAsync<FuelCardsEventResponse>(
            GetApiPoint($"next/{eventId}"),
            HttpMethod.Get,
            token: token)) ?? null;

    public async Task<List<FuelCardsEventResponse>> GetObjectsCardsEvents(string objectName, CancellationToken token)
        => (await http?.SendRequestAsync<List<FuelCardsEventResponse>>(
            GetApiPoint($"objectname/{objectName}"),
            HttpMethod.Get,
            token)) ?? null;
    #endregion

    #region PUT
    public async Task<ResponseBase> PutCardsEvent(RequestSingleAction<FuelCardsEventResponse> item, CancellationToken token)
    => (await http?.SendRequestAsync<RequestSingleAction<FuelCardsEventResponse>, ResponseBase>(
        GetApiPoint(),
        HttpMethod.Put,
        item,
        token)) ?? null;
    #endregion

    #region POST
    public async Task<ResponseSingleAction<FuelCardsEventResponse>> PostCardsEvent(RequestSingleAction<FuelCardsEventResponse> item, CancellationToken token)
        => (await http?.SendRequestAsync<RequestSingleAction<FuelCardsEventResponse>, ResponseSingleAction<FuelCardsEventResponse>>(
            GetApiPoint(),
            HttpMethod.Post,
            item,
            token)) ?? null;
    #endregion

    #region DELETE
    public async Task<ResponseBase> DeleteCardsEvent(FuelCardsEventResponse cardsEvent, CancellationToken token)
        => (await http?.SendRequestAsync<FuelCardsEventResponse, ResponseBase>(
            GetApiPoint("delete"),
            HttpMethod.Post,
            cardsEvent,
            token)) ?? new();

    #endregion
}
