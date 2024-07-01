namespace RSpeditionWEB.Components.Scaffolds.Search
{
    public abstract class SearchTItemsBase<T> : ObjItemsEventBase
    {
        [CascadingParameter]
        public ObjItemsEventBase CascadingParameterRef { get; set; }

        [Parameter]
        public EventCallback<List<T>> SendToParentFoundAndSelectedItems { get; set; }

        [Parameter]
        public List<T> InitParameters { get; set; }

        [Parameter]
        public PropertyInfo KeyPropInfo { get; set; }

        [Parameter]
        public string LabelForSearch { get; set; }

        [Parameter]
        public bool IsRenderLabelForSearch { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        public Type KeyPropInfoType => KeyPropInfo.PropertyType;

        protected bool PropIsNullable => KeyPropInfo.IsNullable();

        protected object GetPropertyValue(T item) => item != null ? KeyPropInfo?.GetValue(item) ??  string.Empty : string.Empty;

        protected string GetKeyValue(T item) => GetPropertyValue(item)?.ToString() ?? string.Empty;

        public List<T> SearchedItems { get; set; } = new();

        public string searchString = string.Empty;
        public string SearchString
        {
            get => this.searchString;
            set
            {
                this.searchString = value;
                Func<Task> f1 = async () => await this.ProgressBarOpen();
                f1();
                Func<Task> f2 = async () => await this.SearchStart();
                f2();
                Func<Task> f3 = async () => await this.ProgressBarClose();
                f3();
            }
        }

        public abstract Task SearchStart();

        new protected async Task ProgressBarOpen() => await this.CascadingParameterRef?.ProgressBarOpenAsync();

        new protected async Task ProgressBarClose() => await this.CascadingParameterRef?.ProgressBarCloseAsync();
    }
}
