using System.Collections.Generic;
using System.Linq;

namespace RSpeditionWEB.Components.ComponentsBase
{
    public class ContextMenuBase<T> : ContextMenuBaseClass<T>
    {
        protected override Coordinate Coordinate { get; set; } = new CoordinateClass<T>();
    }

    public class ContextMenuTableBase<T> : ContextMenuBaseClass<T>
    {
        public PropertyInfo PropSelected { get; set; }

        protected override Coordinate Coordinate { get; set; } = new Coordinate();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected async Task HandleRightClickOnTableHeader(MouseEventArgs args, PropertyInfo pi)
        {
            if (args.Button == 2)
            {
                this.PropSelected = pi;
                this.Coordinate = new Coordinate(args.PageX, args.PageY);
                this.IsRenderContextMenu = true;
            }
        }

        protected override async Task ReactOnSelectPointInContextMenu(string val)
        {
            IsRenderContextMenu = false;

            if (val == "1") // Скрыть колонку
            {
                RemoveColumn(PropSelected);
            }
            else // Отобразить колонку
            {
                PISelectedToRenderStr = string.Empty;
                IsRenderSelectPIAlowedToRender = true;
            }
        }

        protected void AddColumns()
        {
            ParentComponent.ParentComponent.PropertiesRendered = ParentComponent.ParentComponent.InitPropertiesRendered();
            ParentComponent.ParentComponent.MergedCells = ParentComponent.ParentComponent.InitMergedCells();
            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
        }

        protected void RemoveColumns()
        {
            ParentComponent.ParentComponent.PropertiesRendered = new();
            ParentComponent.ParentComponent.MergedCells = new();
            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
        }

        protected void RemoveColumn(PropertyInfo columnToHide = null)
        {
            if (columnToHide != null)
                PropSelected = columnToHide;

            var columnsAfterExclude = ParentComponent?.ParentComponent?.PropertiesRendered?.Where(prop => prop.Name != PropSelected.Name)?.ToList() ?? new();
            ParentComponent.ParentComponent.PropertiesRendered = columnsAfterExclude;

            // Исключить столбец из обьединенных в заголовке таблицы столбцов
            if (IsColumnMergedDefault(PropSelected))
            {
                Dictionary<string, List<PropertyInfo>> mergedCells = new();
                foreach (var pare in ParentComponent?.ParentComponent?.MergedCells ?? new())
                {
                    mergedCells.Add(pare.Key, pare.Value?.Where(cell => cell != PropSelected)?.ToList() ?? new());
                }

                ParentComponent.ParentComponent.MergedCells = mergedCells;
            }

            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
            IsRenderSelectPIAlowedToRender = false;
        }

        protected void RemoveColumns(KeyValuePair<string, List<PropertyInfo>>? group)
        {
            PropSelected = null;
            if (!group.HasValue) return;

            var columnsAfterExclude = ParentComponent?.ParentComponent?.PropertiesRendered?.Where(prop => !group.Value.Value.Any(o => o.Name == prop.Name))?.ToList() ?? new();
            ParentComponent.ParentComponent.PropertiesRendered = columnsAfterExclude;

            var mergedColumnsAfterExclude = ParentComponent?.ParentComponent?.MergedCells?.Where(cell => cell.Key != group.Value.Key)?.ToDictionary(x => x.Key,y => y.Value) ?? new();
            ParentComponent.ParentComponent.MergedCells = mergedColumnsAfterExclude;

            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
            IsRenderSelectPIAlowedToRender = false;
        }

        protected void AddColumns(KeyValuePair<string, List<PropertyInfo>>? group)
        {
            var mergedGroupKey = group.Value.Key;

            foreach(var column in group.Value.Value)
            {
                //
                if (ParentComponent.ParentComponent.MergedCells.ContainsKey(mergedGroupKey))
                {
                    if (!ParentComponent.ParentComponent.MergedCells[mergedGroupKey].Any(cell => cell.Name == column.Name))
                    {
                        ParentComponent?.ParentComponent?.MergedCells[mergedGroupKey].Add(column);
                    }
                }
                else
                {
                    ParentComponent?.ParentComponent?.MergedCells.Add(mergedGroupKey, new List<PropertyInfo> { column });
                }

                //
                PISelectedToRenderStr = column.Name;
                InitSelectedColumn();
                UpdateColumnsOrder();
            }

            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
            IsRenderSelectPIAlowedToRender = false;
        }

        protected void AddColumn(PropertyInfo columnToRender = null)
        {
            if (columnToRender != null)
                PISelectedToRenderStr = columnToRender.Name;

            // Добавить столбец в группу обьединенных в заголовке таблицы столбцов
            if (IsColumnMergedDefault(PISelectedToRender))
            {
                var mergedGroupKey = FindMergedColumnsGroupDefault(PISelectedToRender).Value.Key;
                if(ParentComponent.ParentComponent.MergedCells.ContainsKey(mergedGroupKey)
                    && !ParentComponent.ParentComponent.MergedCells[mergedGroupKey].Any(groupKey => groupKey.Name == PISelectedToRender.Name))
                {
                    ParentComponent?.ParentComponent?.MergedCells[mergedGroupKey].Add(PISelectedToRender);
                }
                else if(!ParentComponent.ParentComponent.MergedCells.ContainsKey(mergedGroupKey))
                {
                    ParentComponent?.ParentComponent?.MergedCells.Add(mergedGroupKey, new List<PropertyInfo> { PISelectedToRender });
                }

                // Если столбец добавляется посредством ПКМ и есть определенное пользователем место для insert
                if (PropSelected != null)
                {
                    // Если выделенный столбец относится к другой обьединенной группе
                    if (IsColumnMergedDefault(PropSelected)
                        && FindMergedColumnsGroup(PropSelected).Value.Key != mergedGroupKey)
                    {
                        var mergedCells = ParentComponent.ParentComponent.MergedCells[mergedGroupKey];
                        if (mergedCells.Count > 1)
                        {
                            PropSelected = ParentComponent.ParentComponent.PropertiesRendered.LastOrDefault(prop => mergedCells.Any(o => o.Name == prop.Name));
                        }
                        else
                        {
                            InitSelectedColumn();
                        }
                    }
                }
                else
                {
                    InitSelectedColumn();
                }
            }
            else
            {
                // Если столбец добавляется посредством ПКМ и есть определенное пользователем место для insert
                if (PropSelected != null)
                {
                    if (IsColumnMergedDefault(PropSelected))
                    {
                        PropSelected = ParentComponent.ParentComponent.PropertiesRendered.LastOrDefault(prop => 
                            FindMergedColumnsGroup(PropSelected).Value.Value.Any(o => o.Name == prop.Name));
                    }
                }
                else
                {
                    InitSelectedColumn();
                }
            }

            UpdateColumnsOrder();

            ParentComponent.ParentComponent.StartFilteringTItemsInvoke();
            IsRenderSelectPIAlowedToRender = false;
        }

        private void InitSelectedColumn()
        {
            var allProps = ParentComponent?.ParentComponent?.InitPropertiesRendered() ?? new();
            var columnToRenderIndex = allProps.IndexOf(PISelectedToRender);
            if (columnToRenderIndex == 0)
                PropSelected = PISelectedToRender;
            else
            {
                while (columnToRenderIndex > 0 && PropSelected == null)
                {
                    --columnToRenderIndex;
                    if (ParentComponent?.ParentComponent?.PropertiesRendered?.Contains(allProps.ElementAt(columnToRenderIndex)) ?? false)
                        PropSelected = allProps.ElementAt(columnToRenderIndex);
                }

                if (PropSelected == null)
                    PropSelected = PISelectedToRender;
            }
        }

        private void UpdateColumnsOrder()
        {
            List<PropertyInfo> result = new();

            if (PropSelected == PISelectedToRender)
            {
                result.Add(PISelectedToRender);
                result.AddRange(ParentComponent?.ParentComponent?.PropertiesRendered ?? new());
            }
            else
            {
                foreach (var pi in ParentComponent?.ParentComponent?.PropertiesRendered ?? new())
                {
                    var index = ParentComponent.ParentComponent.PropertiesRendered.IndexOf(pi);
                    var targetInd = ParentComponent.ParentComponent.PropertiesRendered.IndexOf(PropSelected); // insert after selected column

                    if(!result.Any(prop => prop.Name == pi.Name))
                        result.Add(pi);

                    if (index == targetInd && !result.Any(prop => prop.Name == PISelectedToRender.Name))
                        result.Add(PISelectedToRender);
                }
            }

            this.ParentComponent.ParentComponent.PropertiesRendered = result;
        }
    }

    public abstract class ContextMenuBaseClass<T> : GroupingTItemsBase<T>
    {
        protected bool IsRenderContextMenu { get; set; }
        protected abstract Coordinate Coordinate { get; set; }
        protected virtual async Task ReactOnSelectPointInContextMenu(string val) { }
        protected virtual List<Dictionary<string, string>> ContextMenuPoints { get; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}