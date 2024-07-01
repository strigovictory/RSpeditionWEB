using RSpeditionWEB.Components.Upload;

namespace RSpeditionWEB.Models.ViewModels.Banks
{
    public class BankOperationsReport : UploadedContent, ICloneable
    {
        public BankOperationsReport() : base() { }

        [CustomDisplayAttribute("Счет", "Выберите счет")]
        [Required(ErrorMessage ="Счет обязателен для указания")]
        [JsonIgnore]
        public BillsCurrency BillsCurrency { get; set; }

        [CustomDisplayAttribute("Дата выписки", "Укажите дату выписки")]
        [ValidateDateTime]
        [JsonInclude]
        [JsonPropertyName(nameof(ReportDate))]
        public DateTime? ReportDate { get; set; }

        [CustomDisplayAttribute("Период транзакций, с", "Укажите период транзакций, с")]
        [ValidateDateTime]
        [JsonIgnore]
        public DateTime? StartDate { get; set; }

        [CustomDisplayAttribute("Период транзакций, по", "Укажите период транзакций, по")]
        [ValidateDateTime]
        [JsonIgnore]
        public DateTime? FinishDate { get; set; }

        public override object Clone() => (object)new BankOperationsReport
        {
            FilePath = this.FilePath,
            FileName = this.FileName,
            Content = this.Content,
            UserName = this.UserName,
            BillsCurrency = this.BillsCurrency,
            ReportDate = this.ReportDate,
            StartDate = this.StartDate,
            FinishDate = this.FinishDate,
        };
    }
}
