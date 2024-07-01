using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.Results;

namespace RSpeditionWEB.API
{
    public class FinanceApi : ApiPointsBase
    {
        public FinanceApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options) 
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.finance;

        #region // GET
        public async Task<List<DivisionsBankAccount>> GetDivisionsBankAccounts(CancellationToken token)
        => (await this.http?.SendRequestAsync<List<DivisionsBankAccount>>(
            GetApiPoint(nameof(GetDivisionsBankAccounts)),
            HttpMethod.Get,
            token)) ?? new();

        public async Task<List<BankView>> GetBanks(CancellationToken token)
        => (await http?.SendRequestAsync<List<BankView>>(
            GetApiPoint(nameof(GetBanks)),
            HttpMethod.Get,
            token)) ?? new();

        public async Task<List<EmployeeCreditCardView>> GetEmployeeCreditCards(CancellationToken token)
        => (await http?.SendRequestAsync<List<EmployeeCreditCardView>>(
            GetApiPoint(nameof(GetEmployeeCreditCards)),
            HttpMethod.Get,
            token)) ?? new();

        public async Task<List<BanksCardsOperation_iew>> GetBanksCardsOperations(CancellationToken token)
        => (await http?.SendRequestAsync<List<BanksCardsOperation_iew>>(
            GetApiPoint(nameof(GetBanksCardsOperations)),
            HttpMethod.Get,
            token)) ?? new();

        public async Task<List<BanksCardsOperationKindView>> GetBanksCardsOperationKinds(CancellationToken token)
            => (await http?.SendRequestAsync<List<BanksCardsOperationKindView>>(
            GetApiPoint(nameof(GetBanksCardsOperationKinds)),
            HttpMethod.Get,
            token)) ?? new();
        #endregion

        #region // POST
        public async Task<ResponseGroupActionDetailed<EmployeeCreditCardView, EmployeeCreditCardView>> PostEmployeeCreditCard(List<EmployeeCreditCardView> cards, CancellationToken token)
            => (await http?.SendRequestAsync<List<EmployeeCreditCardView>, ResponseGroupActionDetailed<EmployeeCreditCardView, EmployeeCreditCardView>>(
                GetApiPoint(nameof(PostEmployeeCreditCard)),
                HttpMethod.Post,
                cards,
                token)) ?? new();
        #endregion

        #region // PUT
        public async Task<ResponseSingleAction<BanksCardsOperation_iew>> PutBanksCardsOperation(BanksCardsOperation_iew item, CancellationToken token)
            => (await http?.SendRequestAsync<BanksCardsOperation_iew, ResponseSingleAction<BanksCardsOperation_iew>>(
                GetApiPoint(nameof(PutBanksCardsOperation)),
                HttpMethod.Put,
                item,
                token)) ?? new();

        public async Task<ResponseSingleAction<EmployeeCreditCardView>> PutEmployeeCreditCard(EmployeeCreditCardView card, CancellationToken token)
            => (await http?.SendRequestAsync<EmployeeCreditCardView, ResponseSingleAction<EmployeeCreditCardView>>(
                GetApiPoint(nameof(PutEmployeeCreditCard)),
                HttpMethod.Put,
                card,
                token)) ?? new();

        #endregion

        // UPLOAD
        public async Task<ResponseGroupActionDetailed<BanksCardsOperation_iew, BanksCardsOperation_iew>> UploadBanksCardsReport(BankOperationsReport content, CancellationToken token)
            => (await http?.SendRequestAsync<BankOperationsReport, ResponseGroupActionDetailed<BanksCardsOperation_iew, BanksCardsOperation_iew>>(
                GetApiPoint(nameof(UploadBanksCardsReport)),
                HttpMethod.Post,
                content,
                token)) ?? new();

        #region // DELETE
        public async Task<ResponseBase> DeleteBanksCardsOperations(List<int> itemsIds, CancellationToken token)
            => (await http?.SendRequestAsync<List<int>, ResponseBase>(
                GetApiPoint(nameof(DeleteBanksCardsOperations)),
                HttpMethod.Post,
                itemsIds,
                token)) ?? new();
        #endregion
    }
}
