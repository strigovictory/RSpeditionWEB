namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class MobileReport : UploadedContent, ICloneable
    {
        public MobileReport() : base() { }

        [JsonInclude]
        [JsonPropertyName(nameof(Year))]
        [CustomDisplayAttribute("Год", "Укажите год")]
        public int Year { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Month))]
        [CustomDisplayAttribute("Месяц", "Укажите месяц")]
        public int Month { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(DivisionId))]
        [CustomDisplayAttribute("Подразделение", "Укажите подразделение")]
        [Required(ErrorMessage = "Подразделение обязательно для указания")]
        public int DivisionId { get; set; }

        [JsonIgnore]
        public List<int> PeriodYear => Enumerable.Range(2000, 3000).ToList();

        [JsonIgnore]
        public List<int> PeriodMonth => Enumerable.Range(1, 12).ToList();

        public override object Clone() => new MobileReport
        {
            FilePath = this.FilePath,
            FileName = this.FileName,
            Content = this.Content,
            UserName = this.UserName,
            DivisionId = this.DivisionId,
            Year = this.Year,
            Month = this.Month
        };

        [JsonInclude]
        [JsonPropertyName(nameof(Operator))]
        [CustomDisplayAttribute("Оператор", "Выберите оператора")]
        [Required(ErrorMessage = "Оператор обязателен для указания")]
        public string Operator { get; set; }

        [JsonIgnore]
        public List<string> Operators => new() { "XOMobile" }; 
    }
}
