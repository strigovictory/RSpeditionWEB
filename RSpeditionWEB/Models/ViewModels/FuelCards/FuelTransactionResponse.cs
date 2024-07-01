namespace RSpeditionWEB.Models.ViewModels.FuelCards
{
    [AutoMap(typeof(FuelTransactionShortResponse), ReverseMap = true)]
    [AutoMap(typeof(NotParsedTransaction), ReverseMap = true)]
    [AutoMap(typeof(FuelTransactionFullResponse), ReverseMap = true)]
    public class FuelTransactionResponse : EntityModifiedResponse
    {
        [JsonInclude]
        public int Id { get; set; }

        [JsonInclude]
        public string TransactionID { get; set; }

        [JsonInclude]
        public DateTime OperationDate { get; set; }

        [JsonInclude]
        public decimal? Quantity { get; set; }

        [JsonInclude]
        public decimal? Cost { get; set; }

        [JsonInclude]
        public decimal? TotalCost { get; set; }

        [JsonInclude]
        public bool IsCheck { get; set; }

        [JsonInclude]
        public int ProviderId { get; set; }

        [JsonInclude]
        public int FuelTypeId { get; set; }

        [JsonInclude]
        public int? CurrencyId { get; set; }

        [JsonInclude]
        public int CardId { get; set; }

        [JsonInclude]
        public int? CountryId { get; set; }

        [JsonInclude]
        public int? DriverReportId { get; set; }

        [JsonInclude]
        public bool? IsDaily { get; set; }

        [JsonInclude]
        public bool? IsMonthly { get; set; }

        [JsonInclude]
        public bool? IsCardSelectedManually { get; set; }

        [JsonInclude]
        public int? RegionId { get; set; }

        [JsonInclude]
        public string RegionName { get; set; }

        [JsonInclude]
        public string CheckID { get; set; }

        [JsonInclude]
        public bool? IsStorno { get; set; }

        [JsonInclude]
        public bool? IsModified { get; set; }

        [JsonInclude]
        public string ProviderFuelType { get; set; }

        [JsonInclude]
        public string ProviderFuelTypeName { get; set; }

        public override string ToString()
        {
            return FuelHelper.GetFuelTransactionString(this);
        }
    }
}
