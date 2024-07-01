namespace RSpeditionWEB.Models.ViewModels.Person;

public class AssignmentDutyPeriodResponse
{
    public int? DivisionId { get; set; }

    public string DutyName { get; set; }

    public bool IsHolding { get; set; } // флаг совместительства по этому назначению

    public DateTime DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }
}
