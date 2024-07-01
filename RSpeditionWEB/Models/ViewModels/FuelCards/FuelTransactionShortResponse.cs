namespace RSpeditionWEB.Models.ViewModels.FuelCards;

public class FuelTransactionShortResponse : IComparable
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
    public bool? IsDaily { get; set; }

    [JsonInclude]
    public bool? IsMonthly { get; set; }

    public override string ToString()
    {
        return FuelHelper.GetFuelTransactionString(this);
    }

    public int CompareTo(object obj)
    {
        return (obj is FuelTransactionShortResponse source)
            && source.Id == Id
            && source.TransactionID == TransactionID
            && source.OperationDate == OperationDate
            && source.Quantity == Quantity
            && source.Cost == Cost
            && source.TotalCost == TotalCost ? 0 : -1;
    }
}
