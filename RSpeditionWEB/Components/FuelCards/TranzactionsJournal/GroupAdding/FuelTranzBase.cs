namespace RSpeditionWEB.Components.FuelCards.TranzactionsJournal.GroupAdding
{
    public abstract class FuelTranzBase<T> : GroupEditFormBase<T> where T : class, ICloneable, new()
    {

    }

    public class FuelTranzGroupModel : ICloneable
    {
        public FuelTranzGroupModel() { }

        public Dictionary<string, FuelTranz> Tranzactions { get; set; } = new();

        [ValidateId(PropName = "Провайдер")]
        [CustomDisplayAttribute("Провайдер", "Выберите провайдера")]
        public int FuelProviderId { get; set; }// нужно только для фильтрации топл.карт

        [ValidateId(PropName = "Подразделение")]
        [CustomDisplayAttribute("Подразделение", "Выберите подразделение")]
        public int DivisionId { get; set; } // нужно только для фильтрации топл.карт

        [CustomDisplayAttribute("Услуга", "Выберите разновидность услуги")]
        public int? FuelTypeId { get; set; }

        [CustomDisplayAttribute("Страна", "Выберите страну")]
        public int? CountryId { get; set; }

        private decimal? cost;
        [CustomDisplayAttribute("Цена", "Укажите цену за единицу")]
        public decimal? Cost
        {
            get => this.cost;
            set => this.cost = value.HasValue ? value.Value.RoundingDecimal(2) : value;
        }

        [CustomDisplayAttribute("Валюта", "Выберите валюту")]
        public int? CurrencyId { get; set; }

        public object Clone()
            => new FuelTranzGroupModel
            {
                Tranzactions = new(this.Tranzactions),
                FuelProviderId = this.FuelProviderId,
                DivisionId = this.DivisionId,
                FuelTypeId = this.FuelTypeId,
                CountryId = this.CountryId,
                Cost = this.Cost,
                CurrencyId = this.CurrencyId
            };
    }

    public class FuelTranz : ICloneable
    {
        public FuelTranz() { }

        [CustomDisplayAttribute("Дата", "Укажите дату транзакции")]
        [Required(ErrorMessage = "Укажите дату транзакции")]
        [ValidateDateTime("Дата транзакции")]
        public DateTime? OperationDateDay { get; set; } = DateTime.Now;

        [CustomDisplayAttribute("Время", "Укажите время транзакции")]
        [Required(ErrorMessage = "Укажите время транзакции")]
        public int OperationDateTimeHour { get; set; }

        [Required(ErrorMessage = "Укажите время транзакции, мин.")]
        public int OperationDateTimeMin { get; set; }

        [Required(ErrorMessage = "Укажите время транзакции, сек.")]
        public int OperationDateTimeSec { get; set; }

        private decimal quantity;
        [Required(ErrorMessage = "Укажите количество")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Колличество должно иметь целое значение, которое более {1}.")]
        [CustomDisplayAttribute("Колличество", "Укажите колличество")]
        [ValidateDecimalNotZero(PropName ="Колличество")]
        public decimal Quantity
        {
            get => this.quantity;
            set
            {
                this.quantity = value.RoundingDecimal(2);
                this.TotalCost = this.quantity * this.Cost;
            }
        }

        private decimal? cost;
        [CustomDisplayAttribute("Цена", "Укажите цену")]
        public decimal? Cost
        {
            get => this.cost;
            set
            {
                this.cost = value.HasValue ? value.Value.RoundingDecimal(3) : value;
                this.TotalCost = this.cost * this.Quantity;
            }
        }

        private decimal? totalCost;
        [CustomDisplayAttribute("Стоимость", "Укажите стоимость")]
        public decimal? TotalCost
        {
            get => this.totalCost;
            set
            {
                this.totalCost = value.HasValue ? value.Value.RoundingDecimal(3) : value;
            }
        }

        [ValidateId(PropName = "Карта")]
        [CustomDisplayAttribute("Карта", "Выберите топливную карту")]
        public int CardId { get; set; }

        [ValidateId(PropName = "Услуга")]
        [CustomDisplayAttribute("Услуга", "Выберите разновидность услуги")]
        public int FuelTypeId { get; set; }

        [CustomDisplayAttribute("Валюта", "Выберите валюту")]
        public int? CurrencyId { get; set; }

        [CustomDisplayAttribute("Страна", "Выберите страну, в которой была оказана услуга")]
        public int? CountryId { get; set; }

        public object Clone()
            => new FuelTranz
            {
                OperationDateDay = this.OperationDateDay,
                OperationDateTimeHour = this.OperationDateTimeHour,
                Quantity = this.Quantity,
                Cost = this.Cost,
                TotalCost = this.TotalCost,
                CardId = this.CardId,
                FuelTypeId = this.FuelTypeId,
                CurrencyId = this.CurrencyId,
                CountryId = this.CountryId
            };

        public List<int> TimeRangeHour { get; set; } = System.Linq.Enumerable.Range(0, 24)?.ToList() ?? new();

        public List<int> TimeRangeMin { get; set; } = System.Linq.Enumerable.Range(0, 60)?.ToList() ?? new();

        public List<int> TimeRangeSec { get; set; } = System.Linq.Enumerable.Range(0, 60)?.ToList() ?? new();
    }
}
