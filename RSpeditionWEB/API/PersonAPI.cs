using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    // Класс, для поддержки всех видов http_pars-запросов, касающихся учета сотрудников
    public class PersonApi : ApiPointsBase
    {
        public PersonApi(
            IOptions<GateWayConfigurations> options,
            IHttpService httpService) 
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.employees;

        public async Task<List<EmployeeView>> GetEmployees(CancellationToken token)
           => (await http?.SendRequestAsync<List<EmployeeView>>(
               GetApiPoint(),
               HttpMethod.Get,
               token)) ?? new();

        public async Task<List<EmployeesAssignmentsResponse>> GetEmployeesAssignments(int dutyId, CancellationToken token)
            => (await http?.SendRequestAsync<List<EmployeesAssignmentsResponse>>(
                GetApiPoint($"assignments/{dutyId}"),
                HttpMethod.Get,
                token)) ?? new();
    }
}
