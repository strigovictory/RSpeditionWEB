using RSpeditionWEB.Components.Scaffolds.Graphs;

namespace RSpeditionWEB.Components.Scaffolds.Graphs;

public class ChartsParameters
{
    public ChartsParameters() { }

    public string objectName;

    public string ObjectName
    {
        get => this.objectName;
        set
        {
            this.objectName = value;
            Func<Task> f = async () => await this.ChartsParametersChangedEventInvoke();
            f();
        }
    }

    public string categoryName;
    public string CategoryName
    {
        get => this.categoryName;
        set
        {
            this.categoryName = value;
            Func<Task> f = async () => await this.ChartsParametersChangedEventInvoke();
            f();
        }
    }

    public string subCategoryName;
    public string SubCategoryName
    {
        get => this.subCategoryName;
        set
        {
            this.subCategoryName = value;
            Func<Task> f = async () => await this.ChartsParametersChangedEventInvoke();
            f();
        }
    }

    public delegate Task ChartsParametersChangedDelegate();

    public ChartsParametersChangedDelegate ChartsParametersChangedEvent;
    public async Task ChartsParametersChangedEventInvoke() => await this.ChartsParametersChangedEvent?.Invoke();
}

public abstract class ScaffoldChartBase<T> : ComponentBaseClass
{
    public ScaffoldChart<T> ScaffoldChartRef { get; set; }

    [CascadingParameter]
    public ScaffoldJCMainBase JCMainBaseCascadingParameter { get; set; }

    public PlotlyChartBase<T> ChartModel { get; protected set; }

    public ChartKindPlotly ChartKind { get; set; }

    public List<ChartKindPlotly> ChartsKinds => this.InitChartsKinds();

    public List<ChartKindPlotly> ChartsKindsAlowed => string.IsNullOrEmpty(this.ChartsParameters.subCategoryName)
                                                    ? this.ChartsKinds?.Except(new List<ChartKindPlotly> { ChartKindPlotly.area, ChartKindPlotly.funnel})?.ToList() ?? new ()
                                                    : this.ChartsKinds?.Except(new List<ChartKindPlotly> { ChartKindPlotly.bar, ChartKindPlotly.pie, ChartKindPlotly.line })?.ToList() ?? new()
                                                    ;

    protected virtual List<ChartKindPlotly> InitChartsKinds()
        => new List<ChartKindPlotly> {  ChartKindPlotly.line,
                                        ChartKindPlotly.pie, 
                                        ChartKindPlotly.bar,
                                        ChartKindPlotly.funnel,
                                        ChartKindPlotly.area };

    public List<string> ObjectsNames => this.Init_ObjectsNames();
    protected abstract List<string> Init_ObjectsNames();

    public List<string> CategoriesNames => this.Init_CategoriesNames();
    protected abstract List<string> Init_CategoriesNames();

    public List<string> SubCategoriesNames => this.Init_SubCategoriesNames();
    protected abstract List<string> Init_SubCategoriesNames();

    private ChartsParameters chartsParameters;
    public ChartsParameters ChartsParameters { get; set; }

    protected abstract Task InitChartModel();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
            this.JCMainBaseCascadingParameter.FinishUpdSecondCollectionsEvent += this.InitChartsParameters;
    }
    protected virtual async Task InitDataForCharts()
    => await Task.CompletedTask;

    protected async Task InitChartsParameters()
    {
        try
        {
            await InitDataForCharts();
            this.ChartsParameters = new();
            this.ChartsParameters.objectName = this.ObjectsNames?.FirstOrDefault() ?? string.Empty;
            this.ChartsParameters.categoryName = this.CategoriesNames?.FirstOrDefault() ?? string.Empty;
            this.ChartsParameters.subCategoryName = string.Empty;
            this.ChartKind = this.ChartsKindsAlowed?.FirstOrDefault(_ => (int)_ > 0) ?? ChartKindPlotly.line;
            this.ChartsParameters.ChartsParametersChangedEvent += this.InitChartModelInvoke;
            await this.ChartsParameters.ChartsParametersChangedEventInvoke();
            this.IsRender = true;
            this.StateHasChanged();
        }
        catch (Exception exc)
        {
            var mess = exc?.GetExcMessages(" !");
            this.Message += mess;
            Serilog.Log.Error($"Исключительная ситуация при выполнении метода «{nameof(InitChartsParameters)}» " +
                              $"в классе «{this?.GetType()?.Name ?? string.Empty}». " +
                              $"Тип: «{exc.GetType()?.FullName ?? string.Empty}». " +
                              $"Подробности: {mess} ");
            throw new Exception(mess);
        }
    }

    private async Task InitChartModelInvoke()
    {
        await this.ProgressBarOpenAsync();
        await this.InitChartModel();
        this.ScaffoldChartRef?.InitFunction();
        await this.ProgressBarCloseAsync();
    }
}
