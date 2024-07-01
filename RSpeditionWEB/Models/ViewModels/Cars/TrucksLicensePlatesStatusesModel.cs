namespace RSpeditionWEB.Models.ViewModels.Cars;

public class TrucksLicensePlatesStatusesModel
{
    public int TruckId { get; set; }

    public int DivisionId { get; set; }

    public bool IsDisabled { get; set; }

    public List<TrucksLicensePlatesHistoryModel> TrucksLicensePlates { get; set; } = new();

    public List<TrucksStatusesHistoryModel> TrucksStatuses { get; set; } = new();
}
