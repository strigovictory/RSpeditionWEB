namespace RSpeditionWEB.Components.Banks.BanksCards
{
    public abstract class BanksCardsBase<T> : EditFormBase<T> where T : class, ICloneable, new()
    {
        public List<BankCardType> BankCardTypes
           => new List<BankCardType>
           {
                new BankCardType (1, "UnionPay", "U"),
                new BankCardType (2, "Мир", "R"),
                new BankCardType (3, "MasterCard", "M"),
                new BankCardType (4, "Visa", "V") };
    }

    public class BankCardType : IId
    {
        public int Id { get; set; }

        [CustomDisplayAttribute("Тип карты", "Выберите тип карты")]
        public string Name { get; private set; }

        public string Abbrev { get; private set; }

        public BankCardType()
        {
            this.Name = string.Empty;
            this.Abbrev = string.Empty;
        }

        public BankCardType(int id, string name, string abbr)
        {
            this.Id = id;
            this.Name = name;
            this.Abbrev = abbr;
        }
    }
}
