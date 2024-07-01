namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class PlotlyChartBase<T>
    {
        public string ChartName { get; set; }

        public List<string> DataNames { get; set; }
    }

    public class PlotlyChartGroup<T> : PlotlyChartBase<T>
    {
        public List<string> GroupsNames { get; set; }

        public List<List<T>> DataValues { get; set; }
    }

    public class PlotlyChartSimple<T> : PlotlyChartBase<T>
    {
        public List<T> DataValues { get; set; }
    }
}
