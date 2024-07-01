namespace RSpeditionWEB.Models.ViewModels.Person
{
    [Display(Name = "Водитель")]
    public class DriverView: IComparable
    {
        public DriverView(int id, string name)
        {
            Id = id;
            Name = name ?? string.Empty;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CompareTo(object obj) =>
            (obj is DriverView comparableObj)? comparableObj.Name.CompareTo(Name) : -1;
    }
}
