using RSpeditionWEB.Components.Scaffolds.JC;
using RSpeditionWEB.Components.Scaffolds.Search;

namespace RSpeditionWEB.Components.Scaffolds.JC
{
    public abstract class ScaffoldJCParentBase<T> : FilteringTItemsBase<T>
    {
        [CascadingParameter]
        public ScaffoldJCFiltersBase<T> ParentComponent { get; set; }

        [Parameter]
        public string NumbersFormat { get; set; }

        [Parameter]
        public bool IsRenderOrderNumberColumn { get; set; } = false;

        [Parameter]
        public bool IsRenderCheckColumn { get; set; } = true;

        public SearchTItemsAcrossAllProps<T> SearchTItemsAcrossAllPropsRef { get; set; }

        public ScaffoldJCTable<T> ScaffoldJCTableRef { get; set; }

        protected bool IsRenderSearch { get; set; }

        protected string LabelOnButtonSearch => this.IsRenderSearch ? "Отменить поиск" : "Поиск по всем полям";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.IsRender = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
                this.ParentComponent.TItemsChangedEvent += this.ReactOnEventInParentTTItemsChangedEvent;
        }

        protected void ReactOnEventInParentTTItemsChangedEvent(List<T> items)
        {
            this.TItemsChangedEventInvoke(items ?? new());
        }

        public async Task ClearTableSearchFilters()
        {
            await ProgressBarOpenAsync();
            ParentComponent.ItemsFiltered = ParentComponent.GetFilteredTItems(); // отфильтровать по основным фильтрам (не табличным и не поисковому)
            ParentComponent.UpdateItemsFilteredChecked(); // скинуть все отмеченные строки
            if (SearchTItemsAcrossAllPropsRef != null) // если кнопка поиск открыта и форма рендерится
            {
                SearchTItemsAcrossAllPropsRef.searchString = string.Empty; // очистить поисковую строку
                SearchTItemsAcrossAllPropsRef.SearchedItems = ParentComponent.ItemsFiltered;// очистить поисковый фильтр
            }
            ScaffoldJCTableRef?.ResetMembers(); // сбросить все табличные установки
            ScaffoldJCTableRef?.TItemsChangedEventInvoke(ParentComponent?.ItemsFiltered ?? new()); // запустить событие, чтобы дочерние табличные обновились
            await ProgressBarCloseAsync();
        }
    }
}