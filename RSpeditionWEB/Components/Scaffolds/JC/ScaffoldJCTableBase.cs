using System.Collections.Generic;

namespace RSpeditionWEB.Components.Scaffolds.JC;

public abstract class ScaffoldJCTableBase<T> : ContextMenuTableBase<T>
{
    #region Properies
    [Parameter]
    public string NumbersFormat { get; set; }

    protected Dictionary<PropertyInfo, List<(string name, decimal? value)>> GroupByResults { get; set; } // результат подсчета сумм после группировки

    protected Dictionary<PropertyInfo, decimal> TotalSumResults { get; set; }// результат подсчета общих сумм 

    protected Dictionary<PropertyInfo, SortOrder> SortOrders { get; set; }

    protected (PropertyInfo prop, SortOrder sortOrder) SortOrderLast { get; set; } // последний столбец, по которому сортировалась основная коллекция

    protected bool IsShowDropDownInFirstTh { get; set; } = false;

    protected string ClassNameDropDownInFirstTh => IsShowDropDownInFirstTh ? "show" : "dropdown-content";

    protected Dictionary<PropertyInfo, string> ClassNamesDropDowns { get; set; } = new();

    protected Dictionary<PropertyInfo, string> InputsValues { get; set; } = new();

    protected Dictionary<PropertyInfo, List<object>> OptionsValuesSelected { get; set; } = new();

    protected Dictionary<PropertyInfo, List<object>> OptionsValuesSelectedFilled =>
        this.OptionsValuesSelected?.Where(_ => _.Value.Count > 0)?.ToDictionary(x => x.Key, y => y.Value) ?? new();

    protected Dictionary<PropertyInfo, List<object>> OptionsValuesSearched { get; set; } = new();

    private Dictionary<PropertyInfo, List<object>> OptionsValues = new();

    private Predicate<PropertyInfo> isValType = (prop) => (prop?.GetGetMethod()?.ReturnType?.IsValueType ?? false)
                                                       || (prop?.GetGetMethod()?.ReturnType?.IsEnum ?? false)
                                                       || (prop?.GetGetMethod()?.ReturnType?.IsPrimitive ?? false)
                                                       || (prop?.GetGetMethod()?.ReturnType == typeof(System.String));

    private NumberFormatInfo format = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();

    #endregion

    #region Override methods
    protected override async Task OnInitializedAsync()
    {
        this.format.NumberGroupSeparator = ".";
        this.format.NumberDecimalSeparator = ",";
        await base.OnInitializedAsync();
        ResetMembers();
        this.IsRender = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            this.CheckedCollectionChangedEvent += this.ParentComponent.ParentComponent.ReactOnEventInChild_CollectionCheckedItemsWereChanged;
            this.ParentComponent.TItemsChangedEvent += this.ReactOnEventInParent_TTItemsChangedEvent;
            this.TItemsChangedEvent += this.UpdateMembers;
        }
    }

    private void ReactOnEventInParent_TTItemsChangedEvent(List<T> items)
        => TItemsChangedEventInvoke(ParentComponent?.ParentComponent?.ItemsFiltered ?? new());
    #endregion

    #region Reset methods
    public void ResetMembers()
    {
        SortOrderLast = ParentComponent.ParentComponent.DefaultSortByColumns.FirstOrDefault();
        ResetSortCollection();
        ResetTableHeaderValues();
        ResetGroupByResults();
        ResetTotalSumResults();
        ResetRenderedProperties();
    }

    protected void ResetSortCollection()
    {
        this.SortOrders = new();
        this.ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ =>
        {
            if (!SortOrders.ContainsKey(_)) this.SortOrders?.Add(_, SortOrder.asc);
            else SortOrders[_] = SortOrder.asc;
        });
    }

    private void ResetSortOrders()
    {
        foreach (var pare in SortOrders)
        {
            SortOrders[pare.Key] = SortOrder.asc;
        }
    }

    protected void SortByColumn(PropertyInfo prop, bool isConvertSorting = true)
    {
        ResetClassNames();
        var valOrder = SortOrder.asc;
        if (!SortOrders.ContainsKey(prop)) return;

        if (isConvertSorting)
        {
            valOrder = SortOrders[prop] == SortOrder.asc ? SortOrder.desc : SortOrder.asc;
        }
        else
        {
            valOrder = SortOrders[prop];
        }

        SortOrderLast = (prop, valOrder);

        ResetSortOrders();

        SortOrders[prop] = valOrder;

        Func<T, object> keySelector = (item) => item?.GetType()?.GetProperty(prop.Name)?.GetValue(item);

        if (SortOrders[prop] == SortOrder.asc)
            ParentComponent.ParentComponent.ItemsFiltered = ParentComponent.ParentComponent.ItemsFiltered?.OrderBy(keySelector)?.ToList() ?? new();
        else
            ParentComponent.ParentComponent.ItemsFiltered = ParentComponent.ParentComponent.ItemsFiltered?.OrderByDescending(keySelector)?.ToList() ?? new();
        if (IsGroupByAvailable)
            GroupByColumn(null, false);
    }

    protected void ResetGroupByResults()
    {
        GroupByResults = new();

        if (this.ParentComponent.ParentComponent.GroupByKeySelector != null)
        {
            var groupedItems = ParentComponent.ParentComponent.ItemsFiltered?.GroupBy(this.ParentComponent.ParentComponent.GroupByKeySelector);

            foreach (var prop in this.ParentComponent.ParentComponent.GroupBySumProps ?? new())
            {
                var groupedRes = groupedItems?.Select(_ => (_?.Key?.ToString() ?? String.Empty,
                                                           _?.Sum(value => Decimal.Parse(prop?.GetValue(value)?.ToString() ?? "0"))))?
                                             .OrderByDescending(_ => _.Item2)?.ToList() ?? new();

                if (!this.GroupByResults.ContainsKey(prop))
                    GroupByResults.Add(prop, groupedRes);
                else
                    GroupByResults[prop].AddRange(groupedRes);
            }
        }
    }

    protected void ResetTotalSumResults()
    {
        TotalSumResults = new();

        foreach (var prop in ParentComponent.ParentComponent.TotalSumProps ?? new())
        {
            var totalSumRes = ParentComponent.ParentComponent.ItemsFiltered?.Sum(_ => Decimal.Parse(prop.GetValue(_)?.ToString() ?? "0")) ?? 0;

            if (!TotalSumResults.ContainsKey(prop))
                TotalSumResults.Add(prop, totalSumRes);
            else
                TotalSumResults[prop] = totalSumRes;
        }
    }

    protected void ResetTableHeaderValues()
    {
        ResetClassNames();
        OptionsValues = ResetOptionValues();
        OptionsValuesSearched = new(OptionsValues);
        ResetInputValues();
        ResetOptionValuesSelected();
    }

    protected void HideTableOptionValues()
    {
        IsShowDropDownInFirstTh = false;
        ResetClassNames();
    }

    protected void ResetClassNames()
    {
        ClassNamesDropDowns = new();
        IsRenderContextMenu = false;

        ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ => 
        {
            if (!ClassNamesDropDowns.ContainsKey(_))
                this.ClassNamesDropDowns?.Add(_, "dropdown-content"); 
        });
    }

    private void ResetClassNames(PropertyInfo prop)
    {
        IsRenderContextMenu = false;
        IsShowDropDownInFirstTh = false;

        if (ClassNamesDropDowns.ContainsKey(prop))
        {
            var temp = ClassNamesDropDowns[prop];
            ClassNamesDropDowns = new();
            ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ => this.ClassNamesDropDowns?.Add(_, "dropdown-content"));
            if (ClassNamesDropDowns.ContainsKey(prop))
                ClassNamesDropDowns[prop] = temp == "show" ? "dropdown-content" : "show";
        }
    }

    private Dictionary<PropertyInfo, List<object>> ResetOptionValues()
    {
        Dictionary<PropertyInfo, List<object>> result = new();

        foreach (var propInfo in this.ParentComponent?.ParentComponent?.PropertiesRendered ?? new())
        {
            List<object> objValues = new();
            ParentComponent?.ParentComponent?.ItemsFiltered?.ForEach(_ => objValues.Add(propInfo?.GetValue(_) ?? null));
            result?.Add(propInfo, objValues?.DistinctBy(_ => _?.ToString() ?? string.Empty)?.OrderBy(_ => _)?.ToList() ?? new());
        }

        return result;
    }

    private void ResetInputValues()
    {
        InputsValues = new();
        ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ =>
        {
            if (!InputsValues.ContainsKey(_)) 
                InputsValues?.Add(_, string.Empty);
        });
    }

    private void ResetInputValues(PropertyInfo prop)
    {
        InputsValues = new();
        var temp = InputsValues.ContainsKey(prop) ? InputsValues[prop] : string.Empty;
        ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ =>
        {
            if (!InputsValues.ContainsKey(_))
                InputsValues?.Add(_, string.Empty);
        });
        if(InputsValues.ContainsKey(prop))
            InputsValues[prop] = temp;
    }

    private void ResetOptionValuesSelected()
    {
        OptionsValuesSelected = new();
        ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ =>
        { if (!OptionsValuesSelected.ContainsKey(_))
                OptionsValuesSelected?.Add(_, new());
        });
    }
    #endregion

    #region Update methods
    private void UpdateMembers(List<T> items)
    {
        UpdateMembers();
    }

    private void UpdateMembers()
    {
        ResetClassNames(); // свернуть все options в заголовке таблицы
        ResetInputValues(); // очистить поисковые поля в заголовках таблиц
        OptionsValues = ResetOptionValues(); // актуализировать базовые options
        ApplyFiltersToTItems(); // после актуализации select/options отфильтровать коллекцию
        if (IsGroupByAvailable)
        {
            GroupByColumn(null, false);
        }
    }

    protected void OnClickByTH(MouseEventArgs arg, PropertyInfo prop) => ResetClassNames(prop);
    #endregion

    #region Main methods
    private void ApplyFiltersToTItems()
    {
        var source = ParentComponent?.ParentComponent?.ItemsFiltered ?? new();

        List<T> destination = new();

        source?.ForEach(sourceItem =>
        {
            if (IsVizible(sourceItem))
                destination.Add(sourceItem);
        });

        ParentComponent.ParentComponent.ItemsFiltered = new(destination);
        ParentComponent.ParentComponent.UpdateItemsFilteredChecked();
        OptionsValuesSearched = new(ResetOptionValues()); // актуализировать отображаемые options после  изменения отфильтрованной коллекции
        SortByColumn(SortOrderLast.prop, false);
        ResetGroupByResults();
        ResetTotalSumResults();
    }

    protected void UpdateFilteredTItems()
    {
        ParentComponent?.ParentComponent?.UpdateItemsFiltered();
        ApplyFiltersToTItems();
    }

    private bool IsVizible(T item)
    {
        var isVizible = true;

        try
        {
            foreach (var selectedValues in OptionsValuesSelectedFilled)
            {
                if (!isVizible) // если не выполнилось хотя бы одно условие TItem не включается в итоговую отфильтрованную коллекцию
                    break;

                var isVizibleInner = false;
                var prop = this.ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => _.Name.Equals(selectedValues.Key.Name));
                var propValue = prop?.GetValue(item);

                if (selectedValues
                                  .Value
                                  .Any(_ => !string.IsNullOrEmpty(_?.ToString() ?? string.Empty)
                                  && ((propValue != null && !string.IsNullOrEmpty(propValue?.ToString() ?? string.Empty) && _.ToString().Equals(propValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                         || (_.ToString().Equals("Пустые") && (propValue == null || string.IsNullOrEmpty(propValue?.ToString() ?? string.Empty)))
                                         || (_.ToString().Equals("Не пустые") && propValue != null && !string.IsNullOrEmpty(propValue?.ToString() ?? string.Empty))
                                         || (propValue != null && prop.PropertyType == typeof(bool) && _.ToString() == "Да" && bool.Parse(propValue.ToString()))
                                         || (propValue != null && prop.PropertyType == typeof(bool) && _.ToString() == "Нет" && !bool.Parse(propValue.ToString())))))
                    isVizibleInner = true;

                isVizible = isVizibleInner;
            }
        }
        catch (Exception exc)
        {
            this.Message = exc?.GetExcMessages("! ");
            exc?.LogError(logger, this.GetType()?.Name ?? string.Empty, nameof(IsVizible));
        }

        return isVizible;
    }

    protected async void SearchStart(PropertyInfo prop, string val)
    {
        await this.ProgressBarOpenAsync();
        ResetInputValues(prop);

        OptionsValuesSelected[prop] = new();
        UpdateFilteredTItems();
        OptionsValuesSearched = new(ResetOptionValues());

        if (string.IsNullOrEmpty(val) || string.IsNullOrWhiteSpace(val))
        {
            //OptionsValuesSelected[prop] = OptionsValuesSearched[prop];
        }
        else
        {
            var foundItems = OptionsValuesSearched[prop]?
                .Where(_ => _?.ToString()?.ToLower()?.Contains(val.ToLower()) ?? false)?
                .ToList() ?? new();

            this.OptionsValuesSearched[prop] = new(foundItems);
            OptionsValuesSelected[prop] = new(OptionsValuesSearched[prop]);

            if (this.OptionsValuesSearched[prop]?.Count != 0)
            {
                UpdateFilteredTItems();
                OptionsValuesSearched = new(ResetOptionValues());
                OptionsValuesSelected[prop] = new();
            }
            else
            {
                ParentComponent.ParentComponent.ItemsFiltered = new(); 
                ParentComponent.ParentComponent.ItemsFilteredChecked = new();
                this.OptionsValuesSearched = new();
                this.ParentComponent?.ParentComponent?.PropertiesRendered?.ForEach(_ => this.OptionsValuesSearched?.Add(_, new()));
            }
        }

        if (IsGroupByAvailable)
        {
            GroupByColumn(null, false);
        }

        ResetClassNames(prop);
        await InvokeAsync(this.StateHasChanged);
        await this.ProgressBarCloseAsync();
    }

    protected async Task SelectionChanged(PropertyInfo prop, object selectedValue)
    {
        await ProgressBarOpenAsync();
        ResetInputValues(prop);

        if (selectedValue.ToString().Equals("Выбрать всё"))
        {
            InputsValues[prop] = string.Empty;
            OptionsValuesSelected[prop] = new();
            UpdateFilteredTItems();
        }
        else if (selectedValue.ToString().Equals("Пустые")
              || selectedValue.ToString().Equals("Не пустые"))
        {
            InputsValues[prop] = string.Empty;
            OptionsValuesSelected[prop] = new();
            OptionsValuesSelected[prop].Add(selectedValue);
            UpdateFilteredTItems();
        }
        else
        {
            if (!OptionsValuesSelected[prop].Contains(selectedValue))
                OptionsValuesSelected[prop].Add(selectedValue);
            else
                OptionsValuesSelected[prop] = new(OptionsValuesSelected[prop]?.Where(_ => !_.Equals(selectedValue))?.ToList() ?? new());

            var temp = OptionsValuesSearched[prop];
            UpdateFilteredTItems();
            OptionsValuesSearched[prop] = temp;
        }

        if (IsGroupByAvailable)
        {
            GroupByColumn(null, false);
        }

        ResetClassNames(prop);
        await InvokeAsync(this.StateHasChanged);
        await ProgressBarCloseAsync();
    }
    #endregion
}