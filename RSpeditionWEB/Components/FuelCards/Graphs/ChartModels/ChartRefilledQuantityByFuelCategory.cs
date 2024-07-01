namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartModels
{
    /// <summary>
    /// Класс, представляющий собой модель, содержащую свойства для отрисовки графика заправок за выбранный период с группировкой по подразделениям
    /// </summary>
    public class ChartRefilledQuantityByFuelCategory
    {
        // Категория, к которой относится топливный продукт из транзакции
        public string FuelCategory { get; set; }

        // Колличество заправленного топлива за период, л.
        public decimal RefilledQuantity { get; set; }

        // Конструктор по умолчанию
        public ChartRefilledQuantityByFuelCategory()
        {
            this.RefilledQuantity = 0;
            this.FuelCategory = string.Empty;
        }
    }
}
