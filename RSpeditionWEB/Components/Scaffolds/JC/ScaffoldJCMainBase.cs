namespace RSpeditionWEB.Components.Scaffolds.JC
{
    public abstract class ScaffoldJCMainBase : ComponentBaseClass
    {
        // Ссылки на вкладки траницы Tabs - tckb
        protected ElementReference TabsRef0 { get; set; }
        protected ElementReference TabsRef1 { get; set; }
        protected ElementReference TabsRef2 { get; set; }
        protected ElementReference TabsRef3 { get; set; }
        protected ElementReference TabsRef4 { get; set; }
        protected ElementReference TabsRef5 { get; set; }

        [Parameter]
        public string ActiveTabNum { get; set; }

        public delegate Task UpdatingCollectionsDelegateAsync();
        public bool IsMainCollectionsInitialized { get; set; }

        public bool IsSecondCollectionsInitialized { get; set; }

        // Событие старта обновления основных коллеций из БД
        protected event UpdatingCollectionsDelegateAsync StartUpdatingMainCollectionsEvent;

        // Событие окончания обновления основных коллеций из БД
        public event UpdatingCollectionsDelegateAsync FinishUpdatingMainCollectionsEvent;

        public event UpdatingCollectionsDelegateAsync StartUpdSecondCollectionsEvent;

        // Событие окончания обновления второстепенных коллеций из БД 
        public event UpdatingCollectionsDelegateAsync FinishUpdSecondCollectionsEvent;

        // Метод для запуска события обновления основных коллеций 
        public async Task RaiseEventStartUpdatingMainCollectionsEvent() => await this.StartUpdatingMainCollectionsEvent.Invoke();

        // Метод для запуска события, сигнализирующего об окончании обновления основных коллекции
        public async Task RaiseEvent_FinishUpdatingMainCollectionsEvent() => await this.FinishUpdatingMainCollectionsEvent?.Invoke();

        public async Task RaiseEvent_StartUpdatingSecondCollectionsEvent() => await this.StartUpdSecondCollectionsEvent?.Invoke();

        // Метод для запуска события, сигнализирующего об окончании обновления второстепенных коллекций
        public async Task RaiseEvent_FinishUpdSecondCollectionsEvent() => await this.FinishUpdSecondCollectionsEvent?.Invoke();

        // Метод, отвечающий за обновление главных коллекций из БД, к-е необходимы для отрисовки журналов
        public abstract Task UpdateMainCollections();

        public abstract Task UpdateSecondCollections();

        protected abstract Task InitializeTabs();

        protected List<string> LabelsForTabs { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.ReturnUrl = $"/{this.GetType().Name}";
            await this.InitializeTabs();
            this.InitEvents();
        }

        private void InitEvents()
        {
            this.StartUpdatingMainCollectionsEvent += this.ReactOnEvent_StartUpdatingMainCollectionsEvent;
            this.FinishUpdatingMainCollectionsEvent += this.ReactOnEvent_IsMainCollectInitialized;
            this.StartUpdSecondCollectionsEvent += this.ReactOnEvent_IsSecondCollectInitializedStarted;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                if (string.IsNullOrEmpty(this.ActiveTabNum) && (LabelsForTabs?.Any() ?? false))
                    await this.js.InvokeVoidAsync("defaultOpenPage");
                else if (LabelsForTabs?.Any() ?? false)
                    await this.js.InvokeVoidAsync("someTabClick", this.GetTabElementRef());
                await this.ReactOnEvent_StartUpdatingMainCollectionsEvent();
            }
        }

        private ElementReference GetTabElementRef() => this.ActiveTabNum switch
        {
            nameof(TabsRef0) => this.TabsRef0,
            nameof(TabsRef1) => this.TabsRef1,
            nameof(TabsRef2) => this.TabsRef2,
            nameof(TabsRef3) => this.TabsRef3,
            nameof(TabsRef4) => this.TabsRef4,
            _ => this.TabsRef0
        };

        protected async Task ReactOnEvent_StartUpdatingMainCollectionsEvent()
        {
            await this.UpdateMainCollections(); // запуск события обновления основных коллекций из БД
            await this.RaiseEvent_FinishUpdatingMainCollectionsEvent(); // запуск события окончания обновления основных коллекций из БД
        }

        private async Task ReactOnEvent_IsMainCollectInitialized()
            => this.IsMainCollectionsInitialized = true;

        private async Task ReactOnEvent_IsSecondCollectInitializedStarted()
        {
            await this.UpdateSecondCollections(); 
            this.IsSecondCollectionsInitialized = true;
            await this.RaiseEvent_FinishUpdSecondCollectionsEvent();
        }

        protected virtual async Task TabClickHandler(int tabNum, string tabType, object dotNetObjRef, ElementReference refTab)
        {
            ActiveTabNum = tabType;
            await this.js.InvokeVoidAsync("openPage", tabNum, tabType, refTab, "#565656");
            await this.js.InvokeVoidAsync("GLOBAL.SetDotnetReference", dotNetObjRef);
        }
    }
}
