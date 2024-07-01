#nullable disable

namespace RSpeditionWEB.Models.ViewModels.Rides
{
    [Display(Name = "Отчет водителя")]
    public class RideDriverReportView
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
        [JsonPropertyName(nameof(ReportStatus))]
        public byte ReportStatus { get; set; }

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

        [JsonInclude]
        [JsonPropertyName(nameof(ForwarderId))]
        public int? ForwarderId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(ForwarderName))]
        public string ForwarderName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TruckNumberId))]
        public int TruckNumberId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TruckNumberName))]
        public string TruckNumberName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TrailerNumberId))]
        public int TrailerNumberId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(TrailerNumberName))]
        public string TrailerNumberName { get; set; }
    }

}
