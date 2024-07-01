namespace RSpeditionWEB.API;

public class Apies : FuelApies, IApies
{
    private readonly CountryApi countryApi;
    private readonly DivisionApi divisionApi;
    private readonly CurrencyApi currencyApi;
    private readonly TruckApi truckApi;
    private readonly TrailerApi trailerApi;
    private readonly FinanceApi financeApi;
    private readonly MobileApi mobileApi;
    private readonly KitEventApi kitEventApi;
    private readonly RideApi rideApi;
    private readonly RideDriverReportApi rideDriverReportApi;
    private readonly PersonApi emplApi;

    public Apies(
        FuelCardsApi fuelCardApi,
        FuelCardsEventsApi fuelCardsEventsApi,
        FuelParserApi fuelParserApi,
        FuelProviderApi fuelProviderApi,
        FuelTransactionApi fuelTransactionApi,
        FuelTypeApi fuelTypeApi,
        CountryApi countryApi,
        DivisionApi divisionApi,
        CurrencyApi currencyApi,
        TruckApi truckApi,
        TrailerApi trailerApi,
        FinanceApi financeApi,
        MobileApi mobileApi,
        PersonApi emplApi,
        KitEventApi kitEventApi,
        RideApi rideApi,
        RideDriverReportApi rideDriverReportApi,
        FuelUploadLogApi fuelUploadLogApi
        )
        : base(
            fuelCardApi,
            fuelCardsEventsApi,
            fuelParserApi,
            fuelProviderApi,
            fuelTransactionApi,
            fuelTypeApi,
            fuelUploadLogApi)
    {
        this.countryApi = countryApi;
        this.divisionApi = divisionApi;
        this.currencyApi = currencyApi;
        this.truckApi = truckApi;
        this.trailerApi = trailerApi;
        this.financeApi = financeApi;
        this.mobileApi = mobileApi;
        this.emplApi = emplApi;
        this.kitEventApi = kitEventApi;
        this.rideApi = rideApi;
        this.rideDriverReportApi = rideDriverReportApi;
    }

    public CountryApi CountryApi => this.countryApi;

    public DivisionApi DivisionApi => this.divisionApi;

    public CurrencyApi CurrencyApi => this.currencyApi;

    public TruckApi TruckApi => this.truckApi;

    public TrailerApi TrailerApi => this.trailerApi;

    public FinanceApi FinanceApi => this.financeApi;

    public MobileApi MobileApi => this.mobileApi;

    public PersonApi PersonApi => this.emplApi;

    public KitEventApi KitEventApi => this.kitEventApi;

    public RideApi RideApi => this.rideApi;

    public RideDriverReportApi RideDriverReportApi => this.rideDriverReportApi;
}
