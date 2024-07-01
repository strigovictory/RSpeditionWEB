using AutoMapper.Internal;
using Microsoft.IdentityModel.Tokens;

namespace RSpeditionWEB.Components.Scaffolds.JC
{
    public abstract class ScaffoldJCFiltersBase<T> : ContextMenuBase<T>
    {
        [CascadingParameter]
        public ScaffoldJCMainBase ScaffoldJCMainBaseCascadingParameter { get; set; }

        public ScaffoldJCParent<T> ScaffoldJCParentRef { get; set; }

        public List<T> Items { get; set; } // Колекция экземпляров, полученная из БД

        public List<T> ItemsFiltered { get; set; } = new();  // Колекция экземпляров после применения фильтров

        public List<T> ItemsFilteredChecked { get; set; } = new();  // Колекция экземпляров после применения фильтров, отмеченная/выделенная галочками пользователем

        protected T Model => this.ModelsType?.IsAbstract ?? true ? default : Activator.CreateInstance<T>();

        protected Type ModelsType => typeof(T);

        protected string LableForSearch => this.ModelsType?.GetModelsDisplayNameValue() ?? string.Empty;

        public List<PropertyInfo> PropertiesRendered { get; set; } // Коллекция свойств, значения которых будут отображаться пользователю

        public abstract List<PropertyInfo> InitPropertiesRendered();

        public List<PropertyInfo> PropertiesHidden => InitPropertiesHidden();

        public virtual List<PropertyInfo> InitPropertiesHidden() => new();

        public List<PropertyInfo> PropertiesAlowedToRender
            => InitPropertiesRendered()?.Except(PropertiesRendered ?? new())?.ToList() ?? new();

        private Dictionary<string, List<PropertyInfo>> mergedCells;
        public Dictionary<string, List<PropertyInfo>> MergedCells
        {
            get => this.mergedCells == null ? this.InitMergedCells() : this.mergedCells;
            set => this.mergedCells = value;
        }

        public virtual Dictionary<string, List<PropertyInfo>> InitMergedCells() => new();

        public virtual Dictionary<PropertyInfo, int> MaxWidthCell => new();

        public Dictionary<PropertyInfo, Func<object, string>> PropertiesStylesFormat
            => this.InitPropertiesStylesFormat(); // Коллекция стилей форматирования для свойств

        protected virtual Dictionary<PropertyInfo, Func<object, string>> InitPropertiesStylesFormat() => new();

        public virtual List<(PropertyInfo pi, SortOrder sortOrder)> DefaultSortByColumns 
            => new List<(PropertyInfo pi, SortOrder sortOrder)>
            {
                (typeof(T).GetProperties()?.FirstOrDefault(_ => _.IsPublic() && !_.IsAbstract()), SortOrder.asc)
            };

        public delegate List<T> GetTItemsDelegate(); // делегат для получения отфильтрованной коллекции

        public event GetTItemsDelegate StartFilteringTItemsEvent; // событие фильтрации коллекции

        public List<T> StartFilteringTItemsInvoke() => StartFilteringTItemsEvent?.Invoke() ?? new(); // метод, запускающий событие фильтрации коллекции

        protected OperationsType OperationsType { get; set; }

        protected bool IsRenderMenuActions { get; set; } // Флаг отображения/сокрытия окна с возможностью выбора действий с выбранной картой

        protected bool IsRenderGroupAdding { get; set; }

        protected bool IsRenderGroupDel { get; set; }

        protected void SortByDefaultColumn()
        {
            if ((DefaultSortByColumns?.Count ?? 0) == 0) return;

            // Отсортировать по первому свойству
            var defaultSortByColumns = DefaultSortByColumns;
            var defaultSortByColumnFirst = defaultSortByColumns.ElementAt(0);
            var keySelectorFirst = (T item) => item?.GetType()?.GetProperty(defaultSortByColumnFirst.pi.Name)?.GetValue(item);
            var firstSorted = defaultSortByColumnFirst.sortOrder == SortOrder.asc
                ? ItemsFiltered?.OrderBy(keySelectorFirst)
                : ItemsFiltered?.OrderByDescending(keySelectorFirst);
            var nextSorted = firstSorted;

            // Отсортировать, начиная со следующего после первого, по которому список уже отсортирован
            for (int i = 1; i < PropToSorting.Count; i++)
            {
                var defaultSortByColumnNext = defaultSortByColumns.ElementAt(i);
                var keySelectorNext = (T item) => item?.GetType()?.GetProperty(defaultSortByColumnNext.pi.Name)?.GetValue(item);
                nextSorted = defaultSortByColumnNext.sortOrder == SortOrder.asc
                    ? nextSorted?.ThenBy(keySelectorNext)
                    : nextSorted?.ThenByDescending(keySelectorNext);
            }

            ItemsFiltered = nextSorted?.ToList() ?? new();
        }

        #region Переопределенные события жизненного цикла компонента
        protected override async Task OnInitializedAsync()
        {
            ReturnUrl = ScaffoldJCMainBaseCascadingParameter?.ReturnUrl ?? "/";
            ResetMembers();
            await base.OnInitializedAsync();
        }

        public void ResetMembers()
        {
            ResetFilters();
            PropertiesRendered = InitPropertiesRendered();
            //await InitCreatingLinkedList(); пока функция множественной сортировки недоступна - согласно задаче во всех компонентах
            SortByDefaultColumn();
            InitGroupBySelector();
        }

        protected virtual void InitGroupBySelector()
        {
            this.GroupByKeySelector = null;
            this.GroupBySumProps = new();
            this.TotalSumProps = new();
        }

        protected virtual void InitEvents()
        {
            this.StartFilteringTItemsEvent += this.ApplyFiltersToTItems;
            this.ScaffoldJCMainBaseCascadingParameter.FinishUpdatingMainCollectionsEvent += this.UpdateCollectionTItemsFromParent;
            this.ScaffoldJCMainBaseCascadingParameter.FinishUpdSecondCollectionsEvent += this.ReactOnSecondCollectionsUpdated;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
                InitEvents();
            this.InitButtons();
            if (this.ScaffoldJCMainBaseCascadingParameter.IsMainCollectionsInitialized
                && !this.ScaffoldJCMainBaseCascadingParameter.IsSecondCollectionsInitialized)
                await this.ScaffoldJCMainBaseCascadingParameter.RaiseEvent_StartUpdatingSecondCollectionsEvent();
        }
        #endregion

        protected abstract Task<List<T>> GetCollectionTItemsFromParent();

        protected virtual void GetSecondCollections() { }

        protected virtual async Task UpdateGetMainCollectionTItemsFromDB()
        {
            this.Items = new();
            try
            {
                await this.ScaffoldJCMainBaseCascadingParameter?.UpdateMainCollections();
                await this.UpdateCollectionTItemsFromParent();
            }
            catch (Exception exc)
            {
                exc?.LogError(logger, this.GetType()?.Name ?? string.Empty, nameof(UpdateGetMainCollectionTItemsFromDB));
            }
        }

        public virtual void ResetFilters(bool isNeedReRender = false) { }

        public virtual async Task UpdateCollectionTItemsFromParent()
        {
            this.Items = new(await this.GetCollectionTItemsFromParent() ?? new());
            this.StartFilteringTItemsInvoke(); 
            await this.IsRenderTrueAsync();
        }

        protected List<T> ApplyFiltersToTItems()
        {
            UpdateItemsFiltered();
            StartOrdering();
            InitButtons();
            return ItemsFiltered ?? new();
        }

        public void UpdateItemsFiltered()
        {
            ItemsFiltered = GetFilteredTItems(); // отфильтровать по основным фильтрам
            if (ScaffoldJCParentRef != null && ScaffoldJCParentRef.SearchTItemsAcrossAllPropsRef != null) // при первоначальной отрисовке ссылка равна null
            {
                ItemsFiltered = ScaffoldJCParentRef?.SearchTItemsAcrossAllPropsRef?.SearchItems() ?? new(); // отфильтровать по поисковому фильтру
            }

            UpdateItemsFilteredChecked();
        }

        public void UpdateItemsFilteredChecked()
        {
            ItemsFilteredChecked = ItemsFiltered?.Intersect(ItemsFilteredChecked)?.ToList() ?? new();
            InitButtons();
        }

        public List<T> GetFilteredTItems() => Items?.Where(item => this.IsVizible(item))?.ToList() ?? new();

        protected virtual bool IsVizible(T item) => true;

        public void ReactOnSearch(List<T> searchedItems)
        {
            if ((searchedItems?.Count ?? 0) == 0)
                this.ItemsFiltered = new();
            else
                this.ItemsFiltered = searchedItems ?? new();

            UpdateItemsFilteredChecked();

            if ((ScaffoldJCParentRef?.ScaffoldJCTableRef?.IsGroupByAvailable ?? false))
            {
                ScaffoldJCParentRef?.ScaffoldJCTableRef?.GroupByColumn();
            }

            StartOrdering();
            InitButtons();
        }

        public void StartOrdering()
        {
            //this.CollectionToSort = new(this.ItemsFiltered);  // Отсортировать коллекцию
            //this.ItemsFiltered = new(this.CollectionToSort);  // Вернуть отсортированную коллекцию
            UpdateItemsFilteredChecked();
            TItemsChangedEventInvoke(ItemsFiltered); // запустить событие изменения основной коллекции для дочерних компонентов
        }

        public async Task UpdateAfterAction()
        {
            await this.ProgressBarOpenAsync();
            await this.UpdateGetMainCollectionTItemsFromDB();
            await this.ProgressBarCloseAsync();
        }

        public bool IsActive => this.IsRender && this.ScaffoldJCMainBaseCascadingParameter.IsSecondCollectionsInitialized;
        public Func<T, object> GroupByKeySelector { get; set; } // ключ, по которому выполняется группировка
        public List<PropertyInfo> GroupBySumProps { get; set; } = new(); // коллекция свойств, по которым будут вычисляться суммы после применения группировки
        public List<PropertyInfo> TotalSumProps { get; set; } = new();// коллекция свойств, по которым будут вычисляться общие суммы 

        private async Task ReactOnSecondCollectionsUpdated()
        {
            this.GetSecondCollections();
            this.InitButtons();
            await IsRenderTrueAsync();
        }

        public void ReactOnEventInChild_CollectionCheckedItemsWereChanged(List<T> itemsChecked)
        {
            this.ItemsFilteredChecked = new(itemsChecked);
            this.InitButtons();
            this.StateHasChanged();
        }

        // Метод вызывается из кода .js после события ondrop, примененного к колонке таблицы
        [JSInvokable]
        public async Task MoveColumnToNewPosition(string sourceId, string targetId)
        {
            if (string.IsNullOrEmpty(sourceId) || string.IsNullOrEmpty(targetId)) return;
            var sourcePI = typeof(T).GetProperty(sourceId);
            var targetPI = typeof(T).GetProperty(targetId);
            var sourceInd = this.PropertiesRendered?.IndexOf(sourcePI) ?? 0;
            var targetInd = this.PropertiesRendered?.IndexOf(targetPI) ?? 0;
            List<PropertyInfo> res = new();
            foreach (var pi in this.PropertiesRendered)
            {
                var index = this.PropertiesRendered.IndexOf(pi);

                if (index == targetInd)
                {
                    if (sourceInd > targetInd) // to left
                    {
                        res.Add(sourcePI);
                        res.Add(targetPI);
                    }
                    else // to right
                    {
                        res.Add(targetPI);
                        res.Add(sourcePI);
                    }
                }
                else if (index == sourceInd)
                    continue;
                else
                    res.Add(pi);
            }

            this.PropertiesRendered = res;
            await InvokeAsync(this.StateHasChanged);
        }

        protected void ReactOnSelectItem(T item)
        {
            ItemSelected = item;
            IsRenderMenuActions = true;
        }
    }
}
