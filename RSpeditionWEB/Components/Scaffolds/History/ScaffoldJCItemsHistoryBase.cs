namespace RSpeditionWEB.Components.Scaffolds.History
{
    public abstract class ScaffoldJCItemsHistoryBase<T, V>: ComponentBaseClass
    {
        [Parameter]
        public List<T> HistoryEvents { get; set; }

        [Parameter]
        public EventCallback<(OperationsType, T)> InvokeParentToStartAction { get; set; }

        [CascadingParameter]
        public EventsHistoryBase<T, V> CascadingParameter { get; set; }

        protected List<PropertyInfo> PropertiesRendered { get; private set; }
        protected abstract List<PropertyInfo> GetPropertiesRendered();
        protected Dictionary<Type, List<object>> ItemsFK { get; private set; }
        protected abstract Task<Dictionary<Type, List<object>>> GetItemsFK();
        protected abstract string GetFKValue(PropertyInfo pi, T item);

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.PropertiesRendered = this.GetPropertiesRendered();
            this.ItemsFK = await this.GetItemsFK();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            this.HistoryEvents = this.CascadingParameter?.EventHistoryItems ?? new();
        }
    }
}
