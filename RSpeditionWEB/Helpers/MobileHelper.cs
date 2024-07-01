namespace RSpeditionWEB.Helpers
{
    public static class MobileHelper
    {
        public static List<MobileOperator> MobileOperators
            => new List<MobileOperator> {  new MobileOperator (1, "XOmobile", "X"),
                                           new MobileOperator (2, "SIM-SIM", "S"),
                                           new MobileOperator (3, "Life", "L"),
                                           new MobileOperator (4, "MTC", "M"),
                                           new MobileOperator (5, "Velcom", "V") };


    }
    public class MobileOperator : IId
    {
        public int Id { get; set; }

        [CustomDisplayAttribute("Оператор", "")]
        public string Name { get; private set; }

        public string Abbrev { get; private set; }

        public MobileOperator()
        {
            this.Name = string.Empty;
            this.Abbrev = string.Empty;
        }

        public MobileOperator(int id, string name, string abbr)
        {
            this.Id = id;
            this.Name = name;
            this.Abbrev = abbr;
        }
    }
}
