using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    public class CurrencyApi : ApiPointsBase
    {
        public CurrencyApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options)
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.currencies;

        /// <summary>
        /// Метод для получения коллекции валют зарегистрированных в системе
        /// </summary>
        /// <returns>Коллекция валют</returns>
        public async Task<List<CurrencyResponse>> GetCurrencies(CancellationToken token)
            => (await http?.SendRequestAsync<List<CurrencyResponse>>(
                GetApiPoint(),
                HttpMethod.Get,
                token)) ?? new();

        public async Task<List<CurrencyRateView>> GetCurrencyRates(CancellationToken token)
            => (await http?.SendRequestAsync<List<CurrencyRateView>>(
                GetApiPoint(nameof(GetCurrencyRates)),
                HttpMethod.Get,
                token)) ?? new();
    }
}
