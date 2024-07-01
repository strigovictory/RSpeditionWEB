namespace RSpeditionWEB.Components.ComponentsBase
{
    public class ChildComponentBaseClass : ComponentBaseClass 
    {
        public bool ShouldUpdateParent { get; set; }

        [Parameter]
        public EventCallback InvokeParentHendlerCancel { get; set; }

        [Parameter]
        public EventCallback InvokeParentHendlerSuccess { get; set; }

        [Parameter]
        public EventCallback<object> InvokeParentHendlerAfterDel { get; set; }

        [Parameter]
        public EventCallback<ResponseSingleAction<object>> InvokeParentHendlerAfterAction { get; set; }

        [Parameter]
        public EventCallback<ResponseBase> InvokeParentHendlerResponseBaseAfterAction { get; set; }

        [Parameter]
        public EventCallback<object> InvokeParentHendlerAfterUpd { get; set; }

        [Parameter]
        public EventCallback<object> InvokeParentHendlerAfterCreating { get; set; }

        [Parameter]
        public EventCallback<List<object>> InvokeParentHendlerAfterGroupDel { get; set; }

        [Parameter]
        public EventCallback<List<object>> InvokeParentHendlerAfterGroupCreating { get; set; }

        [Parameter]
        public EventCallback<List<object>> InvokeParentHendlerAfterGroupUpdating { get; set; }

        [Parameter]
        public EventCallback<List<object>> InvokeParentHendlerCheckedItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
