namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartModels
{
    /// <summary>
    /// Класс, представляющий собой модель, содержащую свойства для отрисовки графика заправок за выбранный период с группировкой по подразделениям
    /// </summary>
    public class ChartRefilledQuantityByDivision
    {
        // Подразделение, к которому относится произведенная транзакция по заправке
        public string DivisionName { get; set; }

        // Колличество заправленного топлива за период, л.
        public decimal RefilledQuantity { get; set; }

        // Конструктор по умолчанию
        public ChartRefilledQuantityByDivision()
        {
            this.RefilledQuantity = 0;
            this.DivisionName = string.Empty;
        }
    }
}
