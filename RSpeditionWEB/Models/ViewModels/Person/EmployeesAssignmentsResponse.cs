namespace RSpeditionWEB.Models.ViewModels.Person;

public class EmployeesAssignmentsResponse
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public List<AssignmentDutyPeriodResponse> Assignments { get; set; }
}
