namespace RSpeditionWEB.Models.ViewModels.Rides;

[Display(Name = "Отчет о поездке")]
public class RideView
{
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    [Required(ErrorMessage = "Задайте идентификатор отчета о поездке")]
    [CustomDisplayAttribute("Идентификатор отчета о поездке", "Задайте идентификатор отчета о поездке")]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(RideFullNumber))]
    [Required(ErrorMessage = "Укажите полный номер отчета о поездке")]
    [CustomDisplayAttribute("Полный номер отчета о поездке", "Укажите полный номер отчета о поездке")]
    public string RideFullNumber { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(RideNumber))]
    [Required(ErrorMessage = "Укажите номер отчета о поездке")]
    [CustomDisplayAttribute("Номер отчета о поездке", "Укажите номер отчета о поездке")]
    public int RideNumber { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DateReport))]
    public DateTime? DateReport { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DateOut))]
    public DateTime? DateOut { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DateIn))]
    public DateTime? DateIn { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DivisionId))]
    public int DivisionId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DivisionName))]
    public string DivisionName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DriverId))]
    public int? DriverId { get; set; } // ссылка на работников

    [JsonInclude]
    [JsonPropertyName(nameof(DriverName))]
    public string DriverName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ForwarderId))]
    public int? ForwarderId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ForwarderName))]
    public string ForwarderName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TruckId))]
    public int? TruckId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TruckName))]
    public string TruckName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TrailerId))]
    public int? TrailerId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TrailerName))]
    public string TrailerName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TruckNumberId))]
    public int? TruckNumberId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TruckNumberName))]
    public string TruckNumberName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TrailerNumberId))]
    public int? TrailerNumberId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(TrailerNumberName))]
    public string TrailerNumberName { get; set; }
}
