namespace RSpeditionWEB.Models.ViewModels.Rides;

[Display(Name = "Отчет водителя")]
public class RideDriverReportShortView
{
    [JsonInclude]
    [JsonPropertyName(nameof(Id))]
    [Required(ErrorMessage = "Задайте идентификатор отчета водителя")]
    [CustomDisplayAttribute("Идентификатор отчета водителя", "Задайте идентификатор отчета водителя")]
    public int Id { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(ReportDate))]
    [CustomDisplayAttribute("Дата отчета о поездке", "Укажите дату отчета о поездке")]
    public DateTime? ReportDate { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(StartTime))]
    public DateTime StartTime { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(FinishTime))]
    public DateTime FinishTime { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(RideId))]
    [CustomDisplayAttribute("Идентификатор отчета о поездке", "Задайте идентификатор отчета о поездке")]
    public int? RideId { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(RideName))]
    public string RideName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(DriverId))]
    public int DriverId { get; set; } // ссылка на работников

    [JsonInclude]
    [JsonPropertyName(nameof(DriverName))]
    public string DriverName { get; set; }

    [JsonIgnore]
    public string RideDriverReportName => this.ToString();

    public override string ToString()
        => $"Отчет водителя {this.DriverName} от {this.ReportDate?.FormatingDate() ?? string.Empty} " +
           $"(рейс №{this.RideName ?? String.Empty})";
}
