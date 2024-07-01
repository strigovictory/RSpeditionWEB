using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    public class CountryApi : ApiPointsBase
    {
        public CountryApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options) 
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.countries;

        /// <summary>
        /// Метод для получения коллекции всех зарегистрированных в системе стран - запрос с применением параметров запроса
        /// </summary>
        /// <returns>Коллекция стран</returns>
        public async Task<List<CountryResponse>> GetCountries(CancellationToken token)
            => (await this.http?.SendRequestAsync<List<CountryResponse>>(
                GetApiPoint(),
                HttpMethod.Get,
                token)) ?? new();
    }
}
