namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class MobileComSummaryView
    {
        [JsonInclude]
        [JsonPropertyName(nameof(MobComCost_ByPhone))]
        public List<MobComCost_ByPhone_View> MobComCost_ByPhone { get; set; } = new();

        [JsonInclude]
        [JsonPropertyName(nameof(MobileComCost_ByDriver))]
        public List<MobileComCost_ByDriver_View> MobileComCost_ByDriver { get; set; } = new();
    }

    public class MobComCost_ByPhone_View
    {
        [JsonInclude]
        [JsonPropertyName(nameof(phoneNumber))]
        [CustomDisplayAttribute("Телефон", "")]
        public string phoneNumber { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(truckNumber))]
        [CustomDisplayAttribute("Тягач", "")]
        public string truckNumber { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(divisionName))]
        [CustomDisplayAttribute("Подразделение", "")]
        public string divisionName { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(forwarder))]
        [CustomDisplayAttribute("Экспедитор", "")]
        public string forwarder { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(callsCost))]
        [CustomDisplayAttribute("Стоимость коммуникаций, евро", "")]
        public decimal? callsCost { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(periodicCharge))]
        [CustomDisplayAttribute("Абон.плата, периодич.оплата, евро", "")]
        public decimal? periodicCharge { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(onceCharge))]
        [CustomDisplayAttribute("Абон.плата, разовая оплата, евро", "")]
        public decimal? onceCharge { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(mobCommunications))]
        public virtual List<MobileComView> mobCommunications { get; set; } = new();
    }

    [Display(Name = "Счета за связь")]
    public class MobileComCost_ByDriver_View
    {
        [JsonInclude]
        [JsonPropertyName(nameof(truckId))]
        public virtual int truckId { get; set; }

        [CustomDisplayAttribute("Тягач, гос.номер", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(truckNum))]
        public string truckNum { get; set; }

        [CustomDisplayAttribute("Телефон", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(phoneNum))]
        public string phoneNum { get; set; }

        [CustomDisplayAttribute("Подразделение", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(divisionName))]
        public string divisionName { get; set; }

        [CustomDisplayAttribute("Экспедитор", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(forwarder))]
        public string forwarder { get; set; }

        [CustomDisplayAttribute("Период коммуникаций", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(commPeriod))]
        public string commPeriod { get; set; }

        [CustomDisplayAttribute("Абон.плата", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(userCharge))]
        public decimal? userCharge { get; set; }

        [CustomDisplayAttribute("Водитель в рейсе, период", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(ridesPeriod))]
        public string ridesPeriod { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(driverID))]
        public virtual int driverID { get; set; }

        [CustomDisplayAttribute("Водитель", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(driverFio))]
        public string driverFio { get; set; }

        [CustomDisplayAttribute("Дней в рейсе", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(dayCount))]
        public int dayCount { get; set; }

        [CustomDisplayAttribute("Общая сумма", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(allSum))]
        public decimal allSum { get; set; }

        [CustomDisplayAttribute("Неслужебные", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(notOfficeSum))]
        public decimal notOfficeSum { get; set; }

        [CustomDisplayAttribute("Лимит", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(limit))]
        public decimal limit { get; set; }

        [CustomDisplayAttribute("Вычет из превышения", "")]
        [JsonInclude]
        [JsonPropertyName(nameof(deductedValue))]
        public decimal deductedValue { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(deductedValId))]
        public virtual int deductedValId { get; set; }

        [CustomDisplayAttribute("Превышение", "")] // равно общая сумма - неслжебн. - лимит
        [JsonIgnore]
        public decimal limitOver => this.limit > 0 ? (this.allSum - this.limit - this.notOfficeSum > 0 ? this.allSum - this.limit - this.notOfficeSum: 0) : 0;

        [CustomDisplayAttribute("Удержания", "")]
        [JsonIgnore]
        public decimal holdSum => (this.limitOver - this.deductedValue) > 0 ? Math.Round(this.limitOver - this.deductedValue + this.notOfficeSum) : (Math.Round(this.notOfficeSum) > 0 ? Math.Round(this.notOfficeSum) : 0);

        [JsonInclude]
        [JsonPropertyName(nameof(mobCommunications))]
        public virtual List<MobileComView> mobCommunications { get; set; } = new();
    }
}
