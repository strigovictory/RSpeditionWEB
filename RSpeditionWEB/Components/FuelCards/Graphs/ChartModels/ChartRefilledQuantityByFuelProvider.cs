namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartModels
{
    /// <summary>
    /// Класс, представляющий собой модель, содержащую свойства для отрисовки графика заправок за выбранный период с группировкой по поставщику топлива
    /// </summary>
    public class ChartRefilledQuantityByFuelProvider
    {
        // Поставщик топлива, к которому относится произведенная транзакция по заправке
        public string FuelProviderName { get; set; }

        // Колличество заправленного топлива за период, л.
        public decimal RefilledQuantity { get; set; }

        // Конструктор по умолчанию
        public ChartRefilledQuantityByFuelProvider()
        {
            this.RefilledQuantity = 0;
            this.FuelProviderName = string.Empty;
        }
    }
}
