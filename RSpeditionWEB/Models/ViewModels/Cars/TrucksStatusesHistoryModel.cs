namespace RSpeditionWEB.Models.ViewModels.Cars;

public class TrucksStatusesHistoryModel
{
    public int Id { get; set; }

    public int? StatusId { get; set; }

    public string StatusName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? FinishDate { get; set; }
}