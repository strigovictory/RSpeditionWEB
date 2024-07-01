namespace RSpeditionWEB.Models.ViewModels.Cars
{
    public class TrucksLicensePlatesHistoryModel: IComparable
    {
        public string LicensePlate { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int CompareTo(object obj) =>
             (obj is TrucksLicensePlatesHistoryModel comparableObj) ? comparableObj.LicensePlate.CompareTo(LicensePlate) : -1;
    }
}