namespace RSpeditionWEB.Components.FuelCards.CarFuelCards;

public class TrucksEmployeesChoiseBase<T> : EditFormBase<T> 
    where T : class, ICloneable, new()
{
    // Коллекция доступных обьектов номеров тягачей на дату ввода в эксплуатацию карты - коллекция меняется каждый раз при выборе даты ввода в экспл-ю
    public List<TrucksLicensePlate> AvailableTrucksLicensePlates { get; set; }

    public List<EmployeesAssignmentsResponse> AvailableEmployeesAssignments { get; set; }

    public List<TrucksLicensePlatesStatusesModel> TrucksLicensePlatesStatuses { get; set; }

    public List<EmployeesAssignmentsResponse> EmployeesAssignmentsDrivers { get; set; }

    protected virtual List<TruckResponse> Trucks { get; }

    protected EmployeesAssignmentsResponse EmployeesAssignmentsSelected { get; set; }

    protected virtual async Task<List<TrucksLicensePlatesStatusesModel>> GetTrucksLicensePlatesStatuses() => await Task.FromResult(new List<TrucksLicensePlatesStatusesModel>());

    protected virtual async Task<List<EmployeesAssignmentsResponse>> GetEmployeesAssignmentsDrivers() => await Task.FromResult(new List<EmployeesAssignmentsResponse>());

    protected async Task ActualizeTrucksEmployeesFromDb() => TrucksLicensePlatesStatuses = await GetTrucksLicensePlatesStatuses();

    protected async Task ActualizeEmployeesAssignmentsFromDb() => EmployeesAssignmentsDrivers = await GetEmployeesAssignmentsDrivers();

    public void ActualizeAvailableTrucksLicensePlates(DateTime? date)
    {
        AvailableTrucksLicensePlates = new();

        if (!date.HasValue) return;

        var notDisabledTrucks = TrucksLicensePlatesStatuses?.Where(status => !status.IsDisabled)?.ToList() ?? new();
        foreach (var trucksInfo in notDisabledTrucks)
        {
            if (IsStatusActive(trucksInfo.TrucksStatuses, date.Value.Date)) // если тягач на дату ввода в экспл-ю карты был актывным
            {
                var licansePlateOnIssieDate = GetTrucksLicensePlate(trucksInfo.TrucksLicensePlates, date.Value); // найти номер тягача на дату ввода в экспл-ю топливной карты

                // если история номеров не содержит тягач, взять его номер из таблицы с тягачами
                if (string.IsNullOrEmpty(licansePlateOnIssieDate))
                {
                    licansePlateOnIssieDate = Trucks?.FirstOrDefault(_ => _.Id == trucksInfo.TruckId)?.CarNumber ?? string.Empty;
                }

                AvailableTrucksLicensePlates.Add(new TrucksLicensePlate
                {
                    TruckId = trucksInfo.TruckId,
                    DivisionId = trucksInfo.DivisionId,
                    LicensePlate = licansePlateOnIssieDate
                });
            }
        }
    }

    // Найти статус тягача на дату ввода в экспл-ю карты
    private bool IsStatusActive(List<TrucksStatusesHistoryModel> trucksStatuses, DateTime date)
    {
        var nonActiveStatuses = new List<string> { "Продан", "В аренде", "Привлеченный" };
        TrucksStatusesHistoryModel onIssueDate = default;

        foreach (var statusValue in trucksStatuses?.OrderByDescending(_ => _.StartDate)?.ThenByDescending(_ => _.Id)?.ToList() ?? new())
        {
            if (statusValue.StartDate.Date <= date.Date)
            {
                onIssueDate = statusValue;
                break;
            }
        }

        return onIssueDate != default && !nonActiveStatuses.Any(_ => _.Equals(onIssueDate.StatusName, StringComparison.InvariantCultureIgnoreCase));
    }

    private string GetTrucksLicensePlate(List<TrucksLicensePlatesHistoryModel> trucksLicensePlates, DateTime date)
    {
        // Найти номер тягача на дату ввода в экспл-ю карты
        string onIssueDate = string.Empty;

        foreach (var licensePlateValue in trucksLicensePlates?.OrderByDescending(_ => _.BeginDate)?.ToList() ?? new())
        {
            if (licensePlateValue.BeginDate.Date <= date.Date
                && (!licensePlateValue.EndDate.HasValue || licensePlateValue.EndDate.Value.Date >= date.Date))
            {
                onIssueDate = licensePlateValue.LicensePlate;
                break;
            }
        }

        return onIssueDate;
    }

    public void ActualizeAvailableEmployeesAssignments(DateTime? date)
    {
        AvailableEmployeesAssignments = new();

        if (!date.HasValue) return;

        foreach (var driverInfo in EmployeesAssignmentsDrivers ?? new())
        {
            var assignmentsOnIssieDate = GetEmployeesAssignmentsOnIssueDate(driverInfo.Assignments, date.Value); // найти номер тягача на дату ввода в экспл-ю топливной карты

            if ((assignmentsOnIssieDate?.Count ?? 0) > 0)
            {
                AvailableEmployeesAssignments.Add(
                    new EmployeesAssignmentsResponse
                    {
                        EmployeeId = driverInfo.EmployeeId,
                        EmployeeName = driverInfo.EmployeeName,
                        Assignments = assignmentsOnIssieDate
                    });
            }
        }
    }

    private List<AssignmentDutyPeriodResponse> GetEmployeesAssignmentsOnIssueDate(List<AssignmentDutyPeriodResponse> assignments, DateTime date)
    {
        List<AssignmentDutyPeriodResponse> onIssueDate = new();

        // в одну дату м.б. несколько назначений
        foreach (var assignment in assignments?.OrderByDescending(_ => _.DateBegin)?.ToList() ?? new())
        {
            if (assignment.DateBegin.Date <= date.Date &&
            (!assignment.DateEnd.HasValue 
            || (assignment.DateEnd.HasValue && assignment.DateEnd.Value.Date >= date.Date)))
            {
                onIssueDate.Add(assignment);
            }
        }

        return onIssueDate;
    }
}