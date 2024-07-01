using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace RSpeditionWEB.Models.ViewModels.Mobile;

[Display(Name = "Sim-карта")]
public class SimCardView : IId, ICloneable
{
    public SimCardView()
    { }

    [CustomDisplayAttribute("Идентификационный номер", "Укажите идентификационный номер")]
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    public int Id { get; set; }

    [CustomDisplayAttribute("Дата получения", "Выберите дату получения")]
    [ValidateDateTimeAttribute]
    [JsonInclude]
    [JsonPropertyName(nameof(ReceiveDate))]
    public DateTime ReceiveDate { get; set; }

    [CustomDisplayAttribute("Дата списания", "Выберите дату списания")]
    [Validate_DateTimeNullable("Дата списания")]
    [JsonInclude]
    [JsonPropertyName(nameof(DiscardDate))]
    public DateTime? DiscardDate { get; set; }

    [CustomDisplayAttribute("Списана", "Отметьте, если sim-карта списана")]
    [JsonInclude]
    [JsonPropertyName(nameof(Fdiscarded))]
    public bool Fdiscarded { get; set; }

    [CustomDisplayAttribute("Удалена", "Отметьте, если sim-карта удалена")]
    [JsonInclude]
    [JsonPropertyName(nameof(Fdeleted))]
    public bool Fdeleted { get; set; }

    [Required(ErrorMessage = "Оператор должен быть выбран !")]
    [JsonInclude]
    [JsonPropertyName(nameof(CellularOperator))]
    public virtual string CellularOperator { get; set; }

    [CustomDisplayAttribute("Оператор", "Выберите оператора")]
    [Required(ErrorMessage = "Оператор должен быть выбран !")]
    [JsonInclude]
    [JsonPropertyName(nameof(CellularOperatorName))]
    public string CellularOperatorName { get; set; }

    [CustomDisplayAttribute("Номер sim-карты", "Укажите номер sim-карты")]
    [StringLength(100, ErrorMessage = "Номер должен содержать не более {1} символов !")]
    [JsonInclude]
    [JsonPropertyName(nameof(SimCardNumber))]
    public string SimCardNumber { get; set; }

    [CustomForeignAttribute(typeof(TelephoneView), "Nomber")]
    [CustomDisplayAttribute("Телефонный номер", "Укажите телефонный номер")]
    [ValidateIdAttribute(PropName = "Телефонный номер")]
    [JsonInclude]
    [JsonPropertyName(nameof(FkTelephoneId))]
    public virtual int FkTelephoneId { get; set; }

    [CustomDisplayAttribute("Телефонный номер", "Выберите телефонный номер")]
    [JsonInclude]
    [JsonPropertyName(nameof(TelephoneNumber))]
    public string TelephoneNumber { get; set; }

    [CustomForeignAttribute(typeof(DivisionResponse), "Name")]
    [CustomDisplayAttribute("Подразделение", "Укажите подразделение")]
    [ValidateIdAttribute(PropName = "Подразделение")]
    [JsonInclude]
    [JsonPropertyName(nameof(FkDivisionId))]
    public virtual int FkDivisionId { get; set; }

    [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
    [JsonInclude]
    [JsonPropertyName(nameof(DivisionName))]
    public string DivisionName { get; set; }

    [CustomForeignAttribute(typeof(TruckResponse), "CarNumber")]
    [CustomDisplayAttribute("Тягач", "Выберите тягач")]
    [JsonInclude]
    [JsonPropertyName(nameof(TruckId))]
    public virtual int? TruckId { get; set; }

    [CustomDisplayAttribute("Тягач", "Выберите тягач")]
    [JsonInclude]
    [JsonPropertyName(nameof(TruckName))]
    public string TruckName { get; set; }

    [CustomForeignAttribute(typeof(GpsTrackerView), "Number")]
    [CustomDisplayAttribute("Трекер", "Выберите трекер")]
    [JsonInclude]
    [JsonPropertyName(nameof(TrackerId))]
    public virtual int? TrackerId { get; set; }

    [CustomDisplayAttribute("Трекер", "Выберите трекер")]
    [JsonInclude]
    [JsonPropertyName(nameof(TrackerName))]
    public string TrackerName { get; set; }

    [CustomForeignAttribute(typeof(EmployeeView), "LastName")]
    [CustomDisplayAttribute("Работник", "Выберите работника")]
    [JsonInclude]
    [JsonPropertyName(nameof(EmployeeId))]
    public virtual int? EmployeeId { get; set; }

    [CustomDisplayAttribute("Работник", "Выберите работника")]
    [JsonInclude]
    [JsonPropertyName(nameof(EmployeeName))]
    public string EmployeeName { get; set; }

    public object Clone()
    => new SimCardView
    {
        Id = this?.Id ?? 0,
        ReceiveDate = this?.ReceiveDate ?? default,
        DiscardDate = this?.DiscardDate ?? null,
        Fdiscarded = this?.Fdiscarded ?? default,
        Fdeleted = this?.Fdeleted ?? default,
        CellularOperator = this?.CellularOperator ?? string.Empty,
        CellularOperatorName = this?.CellularOperatorName ?? string.Empty,
        SimCardNumber = this?.SimCardNumber ?? string.Empty,
        FkTelephoneId = this?.FkTelephoneId ?? 0,
        TelephoneNumber = this?.TelephoneNumber ?? string.Empty,
        FkDivisionId = this?.FkDivisionId ?? 0,
        DivisionName = this.DivisionName ?? String.Empty,
        TruckId = this?.TruckId ?? null,
        TruckName = this?.TruckName ?? String.Empty,
        TrackerId = this?.TrackerId ?? null,
        TrackerName = this?.TrackerName ?? String.Empty,
        EmployeeId = this?.EmployeeId ?? null,
        EmployeeName = this?.EmployeeName ?? String.Empty
    };

    public override string ToString()
    => $"№ {this.SimCardNumber ?? "-"}, phone CarNumber: {this.TelephoneNumber ?? "-"}";

    List<MobileOperator> MobileOperators
       => new List<MobileOperator> {  new MobileOperator (1, "XOmobile", "X"),
                                       new MobileOperator (2, "SIM-SIM", "S"),
                                       new MobileOperator (3, "Life", "L"),
                                       new MobileOperator (4, "MTC", "M"),
                                       new MobileOperator (5, "Velcom", "V") };
}
