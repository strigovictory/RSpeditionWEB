using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.ResponseModels;
using static System.Net.WebRequestMethods;

namespace RSpeditionWEB.API;

public class FuelParserApi : ApiPointsBase
{
    #region
    public FuelParserApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fuelparser;
    #endregion

    #region Parse
    public async Task<ResponseDoubleGroupActionDetailed<FuelTransactionShortResponse, NotParsedTransaction>> UploadReport(
        FuelReport report, CancellationToken token)
        => (await http?.SendRequestAsync<FuelReport, ResponseDoubleGroupActionDetailed<FuelTransactionShortResponse, NotParsedTransaction>>(
            GetApiPoint(),
            HttpMethod.Post,
            report,
            token: token)) ?? new();

    public async Task<ResponseDoubleGroupActionDetailed<FuelTransactionShortResponse, NotParsedTransaction>> PostFilledTransactions(
        NotParsedTransactionsFilled transactions,
        CancellationToken token)
        => (await http?.SendRequestAsync<NotParsedTransactionsFilled, ResponseDoubleGroupActionDetailed<FuelTransactionShortResponse, NotParsedTransaction>>(
            GetApiPoint("filled"),
            HttpMethod.Post,
            transactions,
            token)) ?? new();
    #endregion

    public async Task<ResponseGroupActionDetailed<FuelCardNotFoundShortResponse, FuelCardNotFoundShortResponse>> DeleteNotFoundCards(CancellationToken token)
    => (await http?.SendRequestAsync<ResponseGroupActionDetailed<FuelCardNotFoundShortResponse, FuelCardNotFoundShortResponse>>(
        GetApiPoint("notfoundcards/delete"),
        HttpMethod.Get,
        token)) ?? new();
}
