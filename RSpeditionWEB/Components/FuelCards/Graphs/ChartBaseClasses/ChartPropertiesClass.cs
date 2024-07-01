namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartBaseClasses
{
    public abstract class ChartPropertiesClass<T> : ComponentBaseClass
    {
        [CascadingParameter]
        public ChartsFuelParent ChartsFuelParentCascadingVal { get; set; }
        public Dictionary<string, List<T>> Items { get; protected set; } = new();

        // Событие изменения основной коллекции компонента необходимой для построения диаграмм
        public event StateItemsDelegate<Dictionary<string, List<T>>> StateItemsWereChangedEvent;
        protected void InvokeStateItemsWereChangedEvent() => this.StateItemsWereChangedEvent?.Invoke(this.Items ?? new());
        protected SortOrder SubCategorySortOrder { get; set; }
        protected decimal ValueStep { get; set; }
        protected decimal ValueMin { get; set; }
        protected decimal ValueMax { get; set; }
        protected int CategoryStep { get; set; }
        protected int CategoryMin { get; set; }
        protected int CategoryMax { get; set; }

        public abstract void InitializeMinMaxStep();
        protected abstract void InitData();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.InitData();
            this.IsRender = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
                this.ChartsFuelParentCascadingVal.StateComponentWereChanged += this.InitData;
        }
    }
}
