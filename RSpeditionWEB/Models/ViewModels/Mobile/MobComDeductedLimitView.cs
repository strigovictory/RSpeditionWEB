namespace RSpeditionWEB.Models.ViewModels.Mobile
{
    public class MobComDeductedLimitView
    {
        [JsonInclude]
        [JsonPropertyName(nameof(Id))]
        public int Id { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Mmonth))]
        public int Mmonth { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(Myear))]
        public int Myear { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkEmployeeId))]
        public int FkEmployeeId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(FkTruckId))]
        public int FkTruckId { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(DeductedValue))]
        public decimal DeductedValue { get; set; }
    }
}
