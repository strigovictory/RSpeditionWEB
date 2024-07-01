using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.RequestModels;

namespace RSpeditionWEB.API;

public class FuelTransactionApi : ApiPointsBase
{
    #region
    public FuelTransactionApi(
        IHttpService httpService,
        IOptions<GateWayConfigurations> options)
        : base(httpService, options)
    {
    }

    public override ControllersNames ControllerName => ControllersNames.fueltransactions;
    #endregion

    #region GET
    public async Task<List<FuelTransactionFullResponse>> GetTransactions(CancellationToken token, int pageSize = 50000)
    {
        List<FuelTransactionFullResponse> result = new();
        ConcurrentQueue<int> skipValues = new();

        // 1 - Запрос кол-ва транзакций всего в БД
        var uri = GetApiPoint("count");

        var transactionsCount = await http?.SendRequestAsync<long>(uri, HttpMethod.Get, token);
        var resDevide = (int)(transactionsCount / pageSize);

        // 2 - Запрос транзакций с пагинацией 
        uri = GetApiPoint();

        var pagesNum = transactionsCount % pageSize > 0 ? resDevide + 1 : resDevide;

        for (var i = 0; i <= pagesNum; i++)
        {
            skipValues.Enqueue(i * pageSize);
        }

        var tasks = new List<Task>();

        var semaphore = new SemaphoreSlim(32);

        while (skipValues.TryDequeue(out var skipCurrent))
        {
            await semaphore.WaitAsync();
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    var queryByPage = $"{uri}?toTake={pageSize}&toSkip={skipCurrent}";
                    var transactionsByPage = await http?.SendRequestAsync<List<FuelTransactionFullResponse>>(queryByPage, HttpMethod.Get, token);
                    lock (result)
                    {
                        result.AddRange(transactionsByPage ?? new());
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);

        return result ?? new();
    }
    #endregion

    #region PUT
    public async Task<ResponseSingleAction<FuelTransactionShortResponse>> PutTransaction(RequestSingleAction<FuelTransactionResponse> transaction, CancellationToken token)
        => (await http?.SendRequestAsync<RequestSingleAction<FuelTransactionResponse>, ResponseSingleAction<FuelTransactionShortResponse>>(
            GetApiPoint(),
            HttpMethod.Put,
            transaction,
            token)) ?? new();
    #endregion

    #region POST
    public async Task<ResponseGroupActionDetailed<FuelTransactionShortResponse, FuelTransactionShortResponse>> PostTransactions(
        RequestGroupAction<FuelTransactionResponse> transactions,
        CancellationToken token)
        => (await http?.SendRequestAsync<RequestGroupAction<FuelTransactionResponse>, ResponseGroupActionDetailed<FuelTransactionShortResponse, FuelTransactionShortResponse>>(
            GetApiPoint(),
            HttpMethod.Post,
            transactions,
            token)) ?? new();
    #endregion

    #region DELETE
    public async Task<ResponseBase> DeleteTransaction(int transactionId, string userName, CancellationToken token)
        => (await http?.SendRequestAsync<ResponseBase>(
            GetApiPoint($"{transactionId.ToString()}/{userName}"),
            HttpMethod.Delete,
            token)) ?? new();

    public async Task<ResponseBase> DeleteTransactions(RequestGroupAction<int> transactionsIds, CancellationToken token)
        => (await http?.SendRequestAsync<RequestGroupAction<int>, ResponseBase>(
            GetApiPoint("delete"),
            HttpMethod.Post,
            transactionsIds,
            token)) ?? new();

    public async Task<ResponseBase> DeleteFuelTransctionDuplicates(string userName, CancellationToken token, int? fuelProvId = null, int? month = null, int? year = null)
        => (await http?.SendRequestAsync<ResponseBase>(
            GetApiPoint("duplicates") + $"?userName={userName}&fuelProviderId={fuelProvId}&month={month}&year={year}",
            HttpMethod.Delete,
            token)) ?? new();
    #endregion
}
