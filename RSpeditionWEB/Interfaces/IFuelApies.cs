namespace RSpeditionWEB.Interfaces
{
    public interface IFuelApies
    {
        FuelCardsApi FuelCardAPI { get; }
        FuelCardsEventsApi FuelCardsEventsApi { get; }
        FuelParserApi FuelParserApi { get; }
        FuelProviderApi FuelProviderApi { get; }
        FuelTransactionApi FuelTransactionApi { get; }
        FuelTypeApi FuelTypeApi { get; }
        FuelUploadLogApi FuelUploadLogApi { get; }
    }
}
