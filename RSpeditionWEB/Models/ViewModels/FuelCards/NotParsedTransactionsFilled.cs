namespace RSpeditionWEB.Models.ViewModels.FuelCards
{
    public class NotParsedTransactionsFilled : FuelParameters
    {
        public NotParsedTransactionsFilled(int providerId, bool isMonthly, string userName, List<NotParsedTransactionFilled> transactions)
            : base(providerId, isMonthly, userName)
        {
            Transactions = transactions;
        }

        [JsonInclude]
        public List<NotParsedTransactionFilled> Transactions { get; set; }
    }
}
