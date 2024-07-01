namespace RSpeditionWEB.Models.ViewModels.Cars;

public class TrucksLicensePlate
{
    public TrucksLicensePlate() { }

    public TrucksLicensePlate(int truckId, string licensePlate)
    {
        TruckId = truckId;
        LicensePlate = licensePlate;
    }

    public int TruckId { get; set; }

    public int DivisionId { get; set; }

    [CustomDisplayAttribute("Рег.номер автомобиля", "Укажите регистрационный номер автомобиля")]
    public string LicensePlate { get; set; }
}
