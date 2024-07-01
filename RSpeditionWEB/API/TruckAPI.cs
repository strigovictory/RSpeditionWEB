using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    // Класс, для поддержки всех видов http_pars-запросов, касающихся учета машин
    public class TruckApi : ApiPointsBase
    {
        public TruckApi(
            IOptions<GateWayConfigurations> options,
            IHttpService httpService) 
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.trucks;

        #region Get
        /// <summary>
        /// Метод для получения коллекции авто
        /// </summary>
        /// <returns>Коллекция авто</returns>
        public async Task<List<TruckResponse>> GetTrucks(CancellationToken token)
            => (await http?.SendRequestAsync<List<TruckResponse>>(
                GetApiPoint(),
                HttpMethod.Get,
                token)) ?? new();

        public async Task<List<TrucksLicensePlatesStatusesModel>> GetTrucksLicensePlatesStatuses(CancellationToken token)
            => (await http?.SendRequestAsync<List<TrucksLicensePlatesStatusesModel>>(
               GetApiPoint("licenseplatesstatuses"),
               HttpMethod.Get,
               token)) ?? new();
        #endregion
    }
}
