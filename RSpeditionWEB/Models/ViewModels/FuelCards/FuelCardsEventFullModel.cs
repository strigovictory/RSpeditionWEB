namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardsEventFullModel : EntityModifiedResponse, ICloneable
{
    [CustomDisplayAttribute("Идентификатор события", "")]
    public int Id { get; set; }

    [CustomDisplayAttribute("Провайдер", "")]
    public string ProviderName { get; set; } = string.Empty;

    [CustomDisplayAttribute("Идентификатор карты", "")]
    public int CardId { get; set; }

    [CustomDisplayAttribute("Карта", "")]
    public string CardNumber { get; set; } = string.Empty;

    [CustomDisplayAttribute("Полный номер карты", "")]
    public string CardFullNumber { get; set; } = string.Empty;

    [CustomDisplayAttribute("Идентификатор автомобиля", "")]
    public int? CarId { get; set; }

    [CustomDisplayAttribute("Автомобиль", "")]
    public string CarNumber { get; set; } = string.Empty;

    [CustomDisplayAttribute("Автомобиль на дату события", "")]
    public string CarNumberOnEventDate { get; set; } = string.Empty;

    public int? DivisionID { get; set; }

    [CustomDisplayAttribute("Подразделение", "")]
    public string DivisionName { get; set; } = string.Empty;

    [CustomDisplayAttribute("Идентификатор водителя", "")]
    public int? EmployeeId { get; set; }

    [CustomDisplayAttribute("Водитель", "")]
    public string EmployeeName { get; set; } = string.Empty;

    [CustomDisplayAttribute("Идентификатор типа события", "")]
    public int EventTypeId { get; set; }

    [CustomDisplayAttribute("Дата с", "")]
    public DateTime StartDate { get; set; }

    [CustomDisplayAttribute("Дата по", "")]
    public DateTime? FinishDate { get; set; }

    public virtual DateTime FinishDateNotNull => FinishDate == null ? DateTime.MaxValue : FinishDate.Value;

    [CustomDisplayAttribute("Комментарий", "")]
    public string Comment { get; set; }

    public object Clone()
        => new FuelCardsEventFullModel
        {
            Id = this.Id,
            ProviderName = this.ProviderName,
            CardId = this.CardId,
            CardNumber = this.CardNumber,
            CardFullNumber = this.CardFullNumber,
            CarId = this.CarId,
            CarNumber = this.CarNumber,
            CarNumberOnEventDate = this.CarNumberOnEventDate,
            EmployeeId = this.EmployeeId,
            EmployeeName = this.EmployeeName,
            DivisionID = this.DivisionID,
            DivisionName = this.DivisionName,
            StartDate = this.StartDate,
            FinishDate = this.FinishDate,
            EventTypeId = this.EventTypeId,
            Comment = this.Comment,
            ModifiedOn = this.ModifiedOn,
            ModifiedBy = this.ModifiedBy,
        };
}
