namespace RSpeditionWEB.API;

public class FuelApies : IFuelApies
{
    private readonly FuelCardsApi fuelCardApi;
    private readonly FuelCardsEventsApi fuelCardsEventsApi;
    private readonly FuelParserApi fuelParserApi;
    private readonly FuelProviderApi fuelProviderApi;
    private readonly FuelTransactionApi fuelTransactionApi;
    private readonly FuelTypeApi fuelTypeApi;
    private readonly FuelUploadLogApi fuelUploadLogApi;

    public FuelApies(
        FuelCardsApi fuelCardApi,
        FuelCardsEventsApi fuelCardsEventsApi,
        FuelParserApi fuelParserApi,
        FuelProviderApi fuelProviderApi,
        FuelTransactionApi fuelTransactionApi,
        FuelTypeApi fuelTypeApi,
        FuelUploadLogApi fuelUploadLogApi)
    {
        this.fuelCardApi = fuelCardApi;
        this.fuelCardsEventsApi = fuelCardsEventsApi;
        this.fuelParserApi = fuelParserApi;
        this.fuelProviderApi = fuelProviderApi;
        this.fuelTransactionApi = fuelTransactionApi;
        this.fuelTypeApi = fuelTypeApi;
        this.fuelUploadLogApi = fuelUploadLogApi;
    }

    public FuelCardsApi FuelCardAPI => this.fuelCardApi;

    public FuelCardsEventsApi FuelCardsEventsApi => this.fuelCardsEventsApi;

    public FuelParserApi FuelParserApi => this.fuelParserApi;

    public FuelProviderApi FuelProviderApi => this.fuelProviderApi;

    public FuelTransactionApi FuelTransactionApi => this.fuelTransactionApi;

    public FuelTypeApi FuelTypeApi => this.fuelTypeApi;

    public FuelUploadLogApi FuelUploadLogApi => this.fuelUploadLogApi;
}
