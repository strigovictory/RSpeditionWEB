namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class ObjItemsEventBase : ChildComponentBaseClass
    {
        public delegate List<object> GetObjItemsDelegate(); // делегат для получения коллекции

        [Parameter]
        public GetObjItemsDelegate GetObjItems { get; set; } // экземпляр делегата для получения коллекции

        public delegate void ObjItemsChangedDelegate(); // делегат события изменения коллекции

        public event ObjItemsChangedDelegate ObjItemsChangedEvent; // событие изменения коллекции

        public void StartObjItemsChangedEvent() => ObjItemsChangedEvent?.Invoke(); // запуск события изменения коллекции

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public List<ButtonBaseClass> ButtonBaseClassList { get; set; }

        protected virtual void InitButtons() { }

        protected virtual async Task InitButtonsAsync() => await Task.FromResult(true);
    }
}
