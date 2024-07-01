using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.Results;

namespace RSpeditionWEB.API;

// Класс, для поддержки всех видов http_pars-запросов, касающихся учета сим-карт, трекеров, телефонов
public class MobileApi : ApiPointsBase
{
    public MobileApi(
        IOptions<GateWayConfigurations> options,
        IHttpService httpService)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.mobile;

    #region // GET

    public async Task<ResponseForChart<decimal>> GetMobComByPeriod(CancellationToken token, bool isYear)
        => (await http?.SendRequestAsync<ResponseForChart<decimal>>(
            string.Concat(
                GetApiPoint(nameof(GetMobComByPeriod)),
                "?",
                "isYear=",
                isYear ? 1 : 0),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<ResponseForChart<decimal>> GetMobComByKind(CancellationToken token)
     => await http?.SendRequestAsync<ResponseForChart<decimal>>(
         GetApiPoint(nameof(GetMobComByKind)),
         HttpMethod.Get,
         token);

    public async Task<MobileComSummaryView> GetMobComSummary(CancellationToken token, int month, int year)
        => (await http?.SendRequestAsync<MobileComSummaryView>(
            string.Concat(
                GetApiPoint(nameof(GetMobComSummary)),
                "?",
                "month=",
                month.ToString(),
                "&",
                "year=",
                year.ToString()),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<SimCardView>> GetSimCards(CancellationToken token)
        => (await http?.SendRequestAsync<List<SimCardView>>(
            GetApiPoint(nameof(GetSimCards)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<GpsTrackerView>> GetGpsTrackers(CancellationToken token)
        => (await http?.SendRequestAsync<List<GpsTrackerView>>(
            GetApiPoint(nameof(GetGpsTrackers)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<GpsTrackerOperationView>> GetGpsTrackersOperations(int gpsId, CancellationToken token)
        => (await http?.SendRequestAsync<List<GpsTrackerOperationView>>(
            string.Concat(
                GetApiPoint(nameof(GetGpsTrackersOperations)),
                $"/{gpsId}"),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<GpsInvoiceView>> GetGpsInvoices(CancellationToken token)
        => (await http?.SendRequestAsync<List<GpsInvoiceView>>(
            GetApiPoint(nameof(GetGpsInvoices)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<GpstrackerModelView>> GetGpsTrackersModels(CancellationToken token)
        => (await http?.SendRequestAsync<List<GpstrackerModelView>>(
            GetApiPoint(nameof(GetGpsTrackersModels)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<PhoneView>> GetPhones(CancellationToken token)
        => (await http?.SendRequestAsync<List<PhoneView>>(
            GetApiPoint(nameof(GetPhones)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<PhoneModelView>> GetPhonesModels(CancellationToken token)
        => (await http?.SendRequestAsync<List<PhoneModelView>>(
            GetApiPoint(nameof(GetPhonesModels)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<TelephoneView>> GetTelephones(CancellationToken token)
        => (await http?.SendRequestAsync<List<TelephoneView>>(
            GetApiPoint(nameof(GetTelephones)),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<List<SimCardsOperationView>> GetSimCardsOperations(int simCardId, CancellationToken token)
        => (await http?.SendRequestAsync<List<SimCardsOperationView>>(
            string.Concat(
                GetApiPoint(nameof(GetSimCardsOperations)),
                $"/{simCardId}"),
            HttpMethod.Get,
            token)) ?? new();

    public async Task<SimCardsOperationView> GetSimCardsOperationPrevious(int eventId, CancellationToken token)
    => (await http?.SendRequestAsync<SimCardsOperationView>(
        string.Concat(
            GetApiPoint(nameof(GetSimCardsOperationPrevious)),
            "/",
            eventId),
        HttpMethod.Get,
        token)) ?? null;

    public async Task<SimCardsOperationView> GetSimCardsOperationNext(int eventId, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardsOperationView>(
            string.Concat(
                GetApiPoint(nameof(GetSimCardsOperationNext)),
                "/",
                eventId),
            HttpMethod.Get,
            token)) ?? null;
    #endregion

    #region // PUT
    public async Task<ResponseBase> SetLinkOnCarInMobComPeriodic(int year, int month, CancellationToken token)
        => (await http?.SendRequestAsync<ResponseBase>(
            string.Concat(
                GetApiPoint(nameof(GetSimCardsOperationNext)),
                "?",
                "year=",
                year.ToString(),
                "&month=",
                month.ToString()),
            HttpMethod.Get,
            token));
    
    public async Task<ResponseBase> PutMobileCommunication(int mobComId, CancellationToken token)
        => (await http?.SendRequestAsync<ResponseBase>(
            string.Concat(
                GetApiPoint(nameof(PutMobileCommunication)),
                "?",
                "mobComId=",
                mobComId.ToString()),
            HttpMethod.Get,
            token));

    public async Task<ResponseSingleAction<GpsTrackerView>> PutGpsTrackers(GpsTrackerView item, CancellationToken token)
        => (await http?.SendRequestAsync<GpsTrackerView, ResponseSingleAction<GpsTrackerView>>(
            GetApiPoint(nameof(PutGpsTrackers)),
            HttpMethod.Put,
            item,
            token)) ?? new();

    public async Task<ResponseGroupActionDetailed<GpsTrackerView, GpsTrackerView>> PutGpsTrackers(List<GpsTrackerView> items, CancellationToken token)
        => (await http?.SendRequestAsync<List<GpsTrackerView>, ResponseGroupActionDetailed<GpsTrackerView, GpsTrackerView>>(
            GetApiPoint(nameof(PutGpsTrackers)),
            HttpMethod.Put,
            items,
            token)) ?? new();

    public async Task<ResponseSingleAction<SimCardView>> PutSimCard(SimCardView item, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardView, ResponseSingleAction<SimCardView>>(
            GetApiPoint(nameof(PutSimCard)),
            HttpMethod.Put,
            item,
            token)) ?? new();

    public async Task<ResponseBase> PutSimCardsOperation(SimCardsOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardsOperationView, ResponseBase>(
            GetApiPoint(nameof(PutSimCardsOperation)),
            HttpMethod.Put,
            item,
            token)) ?? new();

    public async Task<ResponseBase> PutGpsTrackersOperation(GpsTrackerOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<GpsTrackerOperationView, ResponseBase>(
            GetApiPoint(nameof(PutGpsTrackersOperation)),
            HttpMethod.Put,
            item,
            token)) ?? new();

    public async Task<ResponseSingleAction<GpsInvoiceView>> PutGpsInvoice(GpsInvoiceView invoice, CancellationToken token)
        => (await http?.SendRequestAsync<GpsInvoiceView, ResponseSingleAction<GpsInvoiceView>>(
            GetApiPoint(nameof(PutGpsInvoice)),
            HttpMethod.Put,
            invoice,
            token)) ?? new();

    #endregion

    #region // POST
    public async Task<ResponseBase> ChangeMobComDeductedLimit(MobComDeductedLimitView item, CancellationToken token)
        => (await http?.SendRequestAsync<MobComDeductedLimitView, ResponseBase>(
            GetApiPoint(nameof(ChangeMobComDeductedLimit)),
            HttpMethod.Post,
            item,
            token)) ?? new();

    public async Task<ResponseBase> PostCommunications(List<Communication> items, CancellationToken token)
    {
        MobileReportsUploadsSuccessResult modelToSend = new(items?.Where(_ => _ is Communication_call)?.Cast<Communication_call>()?.ToList() ?? new(),
                                                            items?.Where(_ => _ is Communication_sms)?.Cast<Communication_sms>()?.ToList() ?? new(), 
                                                            items?.Where(_ => _ is Communication_gprs)?.Cast<Communication_gprs>()?.ToList() ?? new());

        return (await http?.SendRequestAsync<MobileReportsUploadsSuccessResult, ResponseBase>(
            GetApiPoint(nameof(PostCommunications)),
            HttpMethod.Post,
            modelToSend,
            token)) ?? new();
    }

    public async Task<ResponseGroupActionDetailed<GpsTrackerView, GpsTrackerView>> PostGpsTrackers(List<GpsTrackerView> items, CancellationToken token)
        => (await http?.SendRequestAsync<List<GpsTrackerView>, ResponseGroupActionDetailed<GpsTrackerView, GpsTrackerView>>(
            GetApiPoint(nameof(PostGpsTrackers)),
            HttpMethod.Post,
            items,
            token)) ?? new();

    public async Task<ResponseGroupActionDetailed<SimCardView, SimCardView>> PostSimCards(List<SimCardView> items, CancellationToken token)
        => (await http?.SendRequestAsync<List<SimCardView>, ResponseGroupActionDetailed<SimCardView, SimCardView>>(
            GetApiPoint(nameof(PostSimCards)),
            HttpMethod.Post,
            items,
            token)) ?? new();

    public async Task<ResponseSingleAction<SimCardsOperationView>> PostSimCardsOperation(SimCardsOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardsOperationView, ResponseSingleAction<SimCardsOperationView>>(
            GetApiPoint(nameof(PostSimCardsOperation)),
            HttpMethod.Post,
            item,
            token)) ?? new();

    public async Task<ResponseSingleAction<GpsTrackerOperationView>> PostGpsTrackersOperation(GpsTrackerOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<GpsTrackerOperationView, ResponseSingleAction<GpsTrackerOperationView>>(
            GetApiPoint(nameof(PostGpsTrackersOperation)),
            HttpMethod.Post,
            item,
            token)) ?? new();

    public async Task<ResponseSingleAction<GpsInvoiceView>> PostGpsInvoice(GpsInvoiceView invoice, CancellationToken token)
        => (await http?.SendRequestAsync<GpsInvoiceView, ResponseSingleAction<GpsInvoiceView>>(
            GetApiPoint(nameof(PostGpsInvoice)),
            HttpMethod.Post,
            invoice,
            token)) ?? new();
    #endregion

    #region // DELETE
    public async Task<ResponseBase> DeleteSimCard(SimCardView item, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardView, ResponseBase>(
            GetApiPoint(nameof(DeleteSimCard)),
            HttpMethod.Delete,
            item,
            token)) ?? new();

    public async Task<ResponseBase> DeleteSimCardsOperation(SimCardsOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<SimCardsOperationView, ResponseBase>(
            GetApiPoint(nameof(DeleteSimCardsOperation)),
            HttpMethod.Delete,
            item,
            token)) ?? new();

    public async Task<ResponseBase> DeleteGpsTrackersOperation(GpsTrackerOperationView item, CancellationToken token)
        => (await http?.SendRequestAsync<GpsTrackerOperationView, ResponseBase>(
            GetApiPoint(nameof(DeleteGpsTrackersOperation)),
            HttpMethod.Delete,
            item,
            token)) ?? new();

    #endregion

    #region// UPLOAD
    public async Task<MobileReportsUploadsResult> UploadMobileReport(MobileReport content, CancellationToken token)
        => (await http?.SendRequestAsync<MobileReport, MobileReportsUploadsResult>(
            GetApiPoint(nameof(UploadMobileReport)),
            HttpMethod.Post,
            content,
            token)) ?? new();
    #endregion
}
