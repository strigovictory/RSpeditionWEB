using RSpeditionWEB.API;

namespace RSpeditionWEB.Interfaces
{
    public interface IApies : IFuelApies
    {
        CountryApi CountryApi { get; }
        DivisionApi DivisionApi { get; }
        CurrencyApi CurrencyApi { get; }
        TruckApi TruckApi { get; }
        TrailerApi TrailerApi { get; }
        FinanceApi FinanceApi { get; }
        MobileApi MobileApi { get; }
        PersonApi PersonApi { get; }
        KitEventApi KitEventApi { get; }
        RideApi RideApi { get; }
        RideDriverReportApi RideDriverReportApi { get; }
    }
}
