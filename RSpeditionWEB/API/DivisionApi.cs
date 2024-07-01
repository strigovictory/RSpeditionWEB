using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    public class DivisionApi : ApiPointsBase
    {
        public DivisionApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options)
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.divisions;

        /// <summary>
        /// Метод для получения коллекции подразделений юрлица
        /// </summary>
        /// <returns>Коллекция подразделений юрлица</returns>
        public async Task<List<DivisionResponse>> GetDivisions(CancellationToken token)
        {
            return (await http?.SendRequestAsync<List<DivisionResponse>>(
                GetApiPoint(),
                HttpMethod.Get,
                token)) ?? new();
        }
    }
}
