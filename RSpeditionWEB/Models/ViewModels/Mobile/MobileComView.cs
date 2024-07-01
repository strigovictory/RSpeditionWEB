using RSpeditionWEB.Enums.Graphics;

namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class MobileComView
    {
        [JsonInclude]
        [JsonPropertyName(nameof(Id))]
        public int Id { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Date))]
        public DateTime Date { get; set; }

        [CustomDisplayAttribute("Дата", "")]
        public string DateName => this.Date.ToString("dd.MM.yy HH:mm:ss");

        [JsonInclude]
        [JsonPropertyName(nameof(Mtype))]
        public int Mtype { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(DestinationPhoneNumber))]
        [CustomDisplayAttribute("Телефон", "")]
        public string DestinationPhoneNumber { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(CallLength))]
        [CustomDisplayAttribute("Длительность", "")]
        public string CallLength { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Cost))]
        [CustomDisplayAttribute("Стоимость", "")]
        public decimal Cost { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(EmployeeNotPay))]
        public bool EmployeeNotPay { get; set; }

        [CustomDisplayAttribute("Оплата", "")]
        [JsonIgnore]
        public string Payer => this.EmployeeNotPay ? "Компания" : "Водитель";

        [JsonIgnore]
        [CustomDisplayAttribute("Тип", "")]
        public ImgView Img_View => new(this.ImgSrc);

        [JsonIgnore]
        public string ImgSrc
            => $"/css/open-iconic/icon/{(this.CommunicationKindImg.TryGetValue((Enum.GetValues<CommunicationKind>()?.ToList() ?? new ()).FirstOrDefault(_ => (int) _ == this.Mtype), out var val) ? val : string.Empty)}";

        [JsonIgnore]
        private Dictionary<CommunicationKind, string> CommunicationKindImg =>
            new Dictionary<CommunicationKind, string>
            {
                    {CommunicationKind.CallsOutgoing, "callOut_icon.png"},
                    {CommunicationKind.CallsIncoming, "callIn_icon.png"},
                    {CommunicationKind.SMS, "sms_icon.png"},
                    {CommunicationKind.GPRS, "gprs_icon.png"}
            };

        public override string ToString()
        => $"на телефон назнач. «{this.DestinationPhoneNumber}» " +
           $"от {this.DateName} " +
           $"на сумму {this.Cost.FormatingDecimalToString5()} ";
    }
}
