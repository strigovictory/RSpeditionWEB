namespace RSpeditionWEB.Models.ViewModels.FuelCards
{
    public class NotParsedTransactionFilled : NotParsedTransaction
    {
        [JsonInclude]
        [JsonPropertyName(nameof(FuelCardIdSelected))]
        public int FuelCardIdSelected { get; set; }

        [JsonInclude]
        [JsonPropertyName(nameof(CarIdSelected))]
        public int CarIdSelected { get; set; }
    }
}
