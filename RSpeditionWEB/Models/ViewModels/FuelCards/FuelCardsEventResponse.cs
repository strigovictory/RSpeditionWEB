namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelCardsEventResponse : EntityModifiedResponse, ICloneable
{
    [JsonInclude]
    [CustomDisplayAttribute("Идентификатор", "Укажите идентификатор")]
    public int Id { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Топливная карта", "Выберите топливную карту")]
    public int CardId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Тягач", "Выберите тягач")]
    public int? CarId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    [ValidateNotNullId(PropName = "Подразделение")]
    public int? DivisionID { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Водитель", "Выберите водителя")]
    public int? EmployeeId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Объект", "Выберите объект")]
    public int EventTypeId { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Дата начала", "Выберите дату начала события")]
    public DateTime StartDate { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Дата окончания", "Выберите дату окончания события")]
    public DateTime? FinishDate { get; set; }

    [JsonInclude]
    [CustomDisplayAttribute("Комментарий", "Укажите комментарий")]
    public string Comment { get; set; }

    public object Clone()
        => new FuelCardsEventResponse
        {
             Id = this.Id,
             CardId = this.CardId,
             CarId = this.CarId,
             EmployeeId = this.EmployeeId,
             DivisionID = this.DivisionID,
             StartDate = this.StartDate,
             FinishDate = this.FinishDate,
             EventTypeId = this.EventTypeId,
             Comment = this.Comment,
             ModifiedOn = this.ModifiedOn,
             ModifiedBy = this.ModifiedBy,
         };
}
