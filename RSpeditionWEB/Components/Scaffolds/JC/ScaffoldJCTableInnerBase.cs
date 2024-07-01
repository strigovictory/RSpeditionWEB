namespace RSpeditionWEB.Components.notUsed
{
    public abstract class ScaffoldJCTableInnerBase<T> : FilteringTItemsBase<T>
    {
        [CascadingParameter]
        public ScaffoldJCTableBase<T> ParentComponent { get; set; }

        [Parameter]
        public string NumbersFormat { get; set; } = "{0:N4}";

        [Parameter]
        public T Item { get; set; }

        [Parameter]
        public int NumRow { get; set; }

        [Parameter]
        public EventCallback<T> SendToParentCheckedItem { get; set; }

        private bool isChecked;

        [Parameter]
        public bool IsChecked
        {
            get => this.isChecked;
            set
            {
                this.isChecked = value;
                this.isCheckedInner = value;
            }
        }

        private bool isCheckedInner;
        protected bool IsCheckedInner
        {
            get => this.isCheckedInner;
            set
            {
                if (this.SendToParentCheckedItem.HasDelegate)
                {
                    Func<Task> f = async () => await SendToParentCheckedItem.InvokeAsync(this.Item);
                    f();
                }
            }
        }

        protected async Task HandleRightClick(MouseEventArgs args)
        {
            if (args.Button == 2 && this.SendToParentSelectedItemRightClick.HasDelegate)
                await this.SendToParentSelectedItemRightClick.InvokeAsync(new CoordinateClass<T>(this.Item, args.PageX, args.PageY));
        }
    }
}
