namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class GroupActionFormBase<T> : ActionForm<T> where T : class, ICloneable, new()
    {
        [Parameter]
        public List<T> Items { get; set; } = new();
        public List<T> ItemsSelected { get; set; } = new();

        public bool ShowConfermationDelete { get; set; }

    }
}
