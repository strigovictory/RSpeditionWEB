namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartModels
{
    /// <summary>
    /// Класс, представляющий собой модель, содержащую свойства для отрисовки графика заправок за выбранный период с заданной периодичностью
    /// </summary>
    public class ChartRefilledQuantityByPeriod
    {
        // Периодичность, с которой отражаются в диаграмме произведенные транзакции по заправке
        public int ChartPeriodicity { get; set; }

        // Колличество заправленного топлива за период, л.
        public decimal RefilledQuantity { get; set; }

        // Конструктор по умолчанию
        public ChartRefilledQuantityByPeriod()
        {
            this.RefilledQuantity = 0;
            this.ChartPeriodicity = 0;
        }
    }
}
