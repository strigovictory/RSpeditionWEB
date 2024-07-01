using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyModel;
using Plotly.Blazor;
using Plotly.Blazor.Traces.SankeyLib;
using RSpeditionWEB.Helpers;
using System.Collections.Generic;

namespace RSpeditionWEB.Components.Scaffolds.JC;

public abstract class GroupingTItemsBase<T> : FilteringTItemsBase<T>
{
    [CascadingParameter]
    public ScaffoldJCParentBase<T> ParentComponent { get; set; }

    protected ElementReference DropZoneRef { get; set; }

    public bool IsGroupByAvailable => GroupByProprties?.Any() ?? false;

    private bool isGroupByZoneRender = false;
    public bool IsGroupByZoneRender
    {
        get => isGroupByZoneRender;
        set
        {
            isGroupByZoneRender = value;
            if (!isGroupByZoneRender)
                ResetGroupBy();
        }
    }

    protected Tree<(object propName, object propValue, List<T> itemsInGroup)> TreeToRender { get; set; }

    protected virtual LinkedList<PropertyInfo> GroupByProprties { get; private set; } = new();

    private string DraggableGroupByButtonId { get; set; }

    protected string DraggableThId { get; set; }

    protected bool IsRenderSelectPIAlowedToRender { get; set; }

    protected string PISelectedToRenderStr { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsGroupByZoneRender && DropZoneRef.Context != null && !string.IsNullOrEmpty(DropZoneRef.Id))
        {
            await this.js.InvokeVoidAsync("initializeGroupByDropZone", this.DropZoneRef, this.DropZoneRef);
        }
    }

    protected PropertyInfo PISelectedToRender => !string.IsNullOrEmpty(this.PISelectedToRenderStr) ? typeof(T).GetProperty(this.PISelectedToRenderStr) ?? null : null;

    protected bool IsColumnMerged(PropertyInfo propInfo)
        => ParentComponent?.ParentComponent?.MergedCells?.Any(_ => _.Value.Any(o => o.Name == propInfo.Name)) ?? false;

    protected bool IsColumnMergedDefault(PropertyInfo propInfo)
        => ParentComponent?.ParentComponent?.InitMergedCells()?.Any(_ => _.Value.Any(o => o.Name == propInfo.Name)) ?? false;

    protected KeyValuePair<string, List<PropertyInfo>>? FindMergedColumnsGroup(PropertyInfo propInfo)
        => ParentComponent?.ParentComponent?.MergedCells?.FirstOrDefault(_ => _.Value.Any(o => o.Name == propInfo.Name));

    protected KeyValuePair<string, List<PropertyInfo>>? FindMergedColumnsGroupDefault(PropertyInfo propInfo)
        => ParentComponent?.ParentComponent?.InitMergedCells()?.FirstOrDefault(_ => _.Value.Any(o => o.Name == propInfo.Name));

    protected bool IsMergedColumnFirst(List<PropertyInfo> mergedGroup, PropertyInfo column)
    {
        Dictionary<PropertyInfo, int> indexes = new();
        foreach (var prop in mergedGroup)
        {
            var foundIndex = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(prop) ?? -1;
            if (foundIndex > -1)
            {
                indexes.Add(prop, foundIndex);
            }
        }

        return indexes.MinBy(_ => _.Value).Key == column;
    }

    protected bool IsMergedColumnFirstDefault(List<PropertyInfo> mergedGroup, PropertyInfo column)
    {
        Dictionary<PropertyInfo, int> indexes = new();
        foreach (var prop in mergedGroup)
        {
            var foundIndex = ParentComponent?.ParentComponent?.InitPropertiesRendered()?.IndexOf(prop) ?? -1;
            if (foundIndex > -1)
            {
                indexes.Add(prop, foundIndex);
            }
        }

        return indexes.MinBy(_ => _.Value).Key == column;
    }

    protected void ResetRenderedProperties()
    {
        if (ParentComponent?.ParentComponent?.PropertiesHidden?.Any() ?? false)
        {
            this.ParentComponent.ParentComponent.PropertiesRendered = this.ParentComponent?.ParentComponent?.InitPropertiesRendered()?
                .Except(this.ParentComponent?.ParentComponent?.PropertiesHidden ?? new())?.ToList() ?? new();
        }

        HideMergedColumnDefault();
    }

    private void HideMergedColumnDefault()
    {
        if (ParentComponent?.ParentComponent?.MergedCells?.Values?.
            Any(_ => _.Any(o => ParentComponent?.ParentComponent?.PropertiesHidden?.Any(z => o == z) ?? false)) ?? false)
        {
            Dictionary<string, List<PropertyInfo>> mergedCells = new();

            foreach (var pare in this.ParentComponent?.ParentComponent?.MergedCells ?? new())
            {
                var toRenderMergedColumns = pare.Value?.Where(merged =>
                    !ParentComponent.ParentComponent.PropertiesHidden.Any(tohide => tohide == merged))?.ToList() ?? new();
                if (toRenderMergedColumns.Count > 0)
                {
                    mergedCells.Add(pare.Key, toRenderMergedColumns);
                }
            }

            this.ParentComponent.ParentComponent.MergedCells = mergedCells;
        }
    }

    #region Drag & Drop Handlers
    protected void HandleDragStart_InGroupByButton(DragEventArgs e, string id)
    {
        e.DataTransfer.EffectAllowed = "move";
        DraggableGroupByButtonId = id;
    }

    protected void HandleDragOver_InGroupByButton(DragEventArgs e)
    {
        e.DataTransfer.DropEffect = "move";
    }

    protected void HandleDragStart_th(DragEventArgs e, string id)
    {
        e.DataTransfer.EffectAllowed = "move";
        DraggableThId = id;
    }

    protected void HandleDragOver_th(DragEventArgs e)
    {
        e.DataTransfer.DropEffect = "move";
    }

    protected async Task MoveColumnToNewPosition(string targetId)
    {
        if (string.IsNullOrEmpty(DraggableThId) || string.IsNullOrEmpty(targetId)) return;

        int sourceInd = 0;
        int targetInd = 0;
        PropertyInfo sourcePI = default;
        PropertyInfo targetPI = default;

        // если перетаскивается группа столбцов
        if (ParentComponent?.ParentComponent?.MergedCells?.ContainsKey(DraggableThId) ?? false)
        {
            var sourceGroup = ParentComponent?.ParentComponent?.MergedCells[DraggableThId] ?? new();

            // если перетаскивается в группу столбцов
            if (ParentComponent?.ParentComponent?.MergedCells?.ContainsKey(targetId) ?? false)
            {
                var targetGroup = ParentComponent?.ParentComponent?.MergedCells[targetId];

                targetPI = (ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(sourceGroup.FirstOrDefault()) ?? 0) 
                    > (ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetGroup.FirstOrDefault()) ?? 0)
                ? ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => targetGroup.Any(o => o.Name == _.Name))
                : ParentComponent?.ParentComponent?.PropertiesRendered?.LastOrDefault(_ => targetGroup.Any(o => o.Name == _.Name));

                targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;
            }
            else
            {
                targetPI = typeof(T).GetProperty(targetId);
                targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;

                if (IsColumnMerged(targetPI)) // если перетаскивается в другую сгруппированную область
                {
                    var targetGroup = FindMergedColumnsGroup(targetPI).Value.Value;

                    targetPI = (ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(sourceGroup.FirstOrDefault()) ?? 0) > targetInd
                    ? ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => targetGroup.Any(o => o.Name == _.Name))
                    : ParentComponent?.ParentComponent?.PropertiesRendered?.LastOrDefault(_ => targetGroup.Any(o => o.Name == _.Name));

                    targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;
                }
            }

            foreach (var prop in sourceGroup)
            {
                sourcePI = prop;
                sourceInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(sourcePI) ?? 0;
                ParentComponent.ParentComponent.PropertiesRendered = OrderPropertiesRendered(sourceInd, sourcePI, targetInd, targetPI);
                targetInd++;
                if(targetInd < (ParentComponent?.ParentComponent?.PropertiesRendered?.Count ?? 0))
                targetPI = ParentComponent?.ParentComponent?.PropertiesRendered?.ElementAt(targetInd);
            }
        }
        else 
        {
            sourcePI = typeof(T).GetProperty(DraggableThId);
            sourceInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(sourcePI) ?? 0;

            if (IsColumnMerged(sourcePI)) // если перетаскивается сгруппированный столбец
            {
                var sourceGroup = FindMergedColumnsGroup(sourcePI);
                if (ParentComponent?.ParentComponent?.MergedCells?.ContainsKey(targetId) ?? false)
                {
                    var targetGroup = ParentComponent?.ParentComponent?.MergedCells[targetId];
                    if (sourceGroup.Value.Key != targetId) return; // сгруппированные столбцы можно передвигать в пределах одной групппы
                }
                else if(IsColumnMerged(typeof(T).GetProperty(targetId))) // если перетаскивается в другую сгруппированную область
                {
                    var targetGroup = FindMergedColumnsGroup(typeof(T).GetProperty(targetId));
                    if (sourceGroup.Value.Key != targetGroup.Value.Key) return; // сгруппированные столбцы можно передвигать в пределах одной групппы
                }
                else
                {
                    return;
                }
            }

            // если перетаскивается в группу столбцов
            if (ParentComponent?.ParentComponent?.MergedCells?.ContainsKey(targetId) ?? false)
            {
                var targetGroup = ParentComponent?.ParentComponent?.MergedCells[targetId];

                targetPI = sourceInd > (ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetGroup.FirstOrDefault()) ?? 0)
                ? ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => targetGroup.Any(o => o.Name == _.Name))
                : ParentComponent?.ParentComponent?.PropertiesRendered?.LastOrDefault(_ => targetGroup.Any(o => o.Name == _.Name));

                targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;
            }
            else
            {
                targetPI = typeof(T).GetProperty(targetId);
                targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;

                if (IsColumnMerged(targetPI)) // если перетаскивается в другую сгруппированную область
                {
                    var targetGroup = FindMergedColumnsGroup(targetPI).Value.Value;

                    targetPI = sourceInd > targetInd
                    ? ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => targetGroup.Any(o => o.Name == _.Name))
                    : ParentComponent?.ParentComponent?.PropertiesRendered?.LastOrDefault(_ => targetGroup.Any(o => o.Name == _.Name));

                    targetInd = ParentComponent?.ParentComponent?.PropertiesRendered?.IndexOf(targetPI) ?? 0;
                }
            }

            if (sourceInd == targetInd) return;
            ParentComponent.ParentComponent.PropertiesRendered = OrderPropertiesRendered(sourceInd, sourcePI, targetInd, targetPI);
        }


        DraggableThId = string.Empty;
        await InvokeAsync(StateHasChanged);
    }

    private List<PropertyInfo> OrderPropertiesRendered(int sourceInd, PropertyInfo sourcePI, int targetInd, PropertyInfo targetPI)
    {
        List<PropertyInfo> result = new();

        for (var i = 0; i < (ParentComponent?.ParentComponent?.PropertiesRendered?.Count ?? 0); i++)
        {
            var index = i;
            var pi = ParentComponent?.ParentComponent?.PropertiesRendered?.ElementAt(index);
        
            if (index == targetInd)
                {
                    if (sourceInd > targetInd) // to left
                    {
                        result.Add(sourcePI);
                        result.Add(targetPI);
                    }
                    else // to right
                    {
                        result.Add(targetPI);
                        result.Add(sourcePI);
                    }
                }
                else if (index == sourceInd)
                    continue;
                else
                    result.Add(pi);
            }

        return result;
    }

    protected void HandleClickClose_RemoveFromGroupBy(string targetId)
    {
        if (string.IsNullOrEmpty(targetId)) return;
        var typeName = typeof(T).Name;
        var typeNameLenth = typeName.Length;
        var idPart = "_group_by_id";
        var target = targetId.Contains(idPart) && targetId.Contains(typeName)
            ? targetId?.Substring(typeNameLenth, targetId.Length - typeNameLenth - idPart.Length)
            : targetId;

        var target_prop = GroupByProprties?.FirstOrDefault(_ => _.Name == target);
        if (GroupByProprties?.Contains(target_prop) ?? false)
        {
            if(GroupByProprties?.Remove(target_prop) ?? false)
            {
                GroupByColumn();
            }
        }
    }

    protected void HandleDrop_InGroupByButton(string targetId)
    {
        if (string.IsNullOrEmpty(targetId)) return;
        var source = string.Empty;
        var typeName = typeof(T).Name;
        var typeNameLenth = typeName.Length;
        var idPart = "_group_by_id";

        if (string.IsNullOrEmpty(DraggableGroupByButtonId))
        {
            source = DraggableThId;
        }
        else
        {
            source = DraggableGroupByButtonId.Contains(idPart) && DraggableGroupByButtonId.Contains(typeName)
                ? DraggableGroupByButtonId?.Substring(typeNameLenth, DraggableGroupByButtonId.Length - typeNameLenth - idPart.Length)
                : DraggableGroupByButtonId;
        }

        var target = targetId.Contains(idPart) && targetId.Contains(typeName)
            ? targetId?.Substring(typeNameLenth, targetId.Length - typeNameLenth - idPart.Length)
            : targetId;

        var source_prop = GroupByProprties?.FirstOrDefault(_ => _.Name == source);
        var target_prop = GroupByProprties?.FirstOrDefault(_ => _.Name == target);

        if (GroupByProprties.Contains(source_prop) && GroupByProprties.Contains(target_prop))
        {
            var source_index = GroupByProprties?.ToList()?.IndexOf(source_prop);
            var target_index = GroupByProprties?.ToList()?.IndexOf(target_prop);
            GroupByProprties?.Remove(source_prop);
            var node_source_prop = new LinkedListNode<PropertyInfo>(source_prop);
            var node_target_prop = GroupByProprties.Find(target_prop);

            if (source_index > target_index)
            {
                GroupByProprties?.AddBefore(node_target_prop, node_source_prop);
            }
            else
            {
                GroupByProprties?.AddAfter(node_target_prop, node_source_prop);
            }
        }

        DraggableThId = string.Empty;
        DraggableGroupByButtonId = string.Empty;
        GroupByColumn();
    }

    protected void HandleDrop_InGroupByButtonDropZone()
    {
        // можно перетаскивать только по одной колонке для группировки
        if (string.IsNullOrEmpty(DraggableThId) 
            || (!ParentComponent?.ParentComponent?.PropertiesRendered?.Any(_ => _.Name == DraggableThId) ?? false))
        {
            return;
        }
        else // если перемещаем заголовок таблицы в дроп-зону
        {
            var source_prop = ParentComponent?.ParentComponent?.PropertiesRendered?.FirstOrDefault(_ => _.Name == DraggableThId);

            var foundGroupByItem = GroupByProprties?.FirstOrDefault(_ => _ == source_prop);

            if (foundGroupByItem == null)
            {
                var node_source_prop = new LinkedListNode<PropertyInfo>(source_prop);
                GroupByProprties?.AddLast(node_source_prop);
            }

            DraggableThId = string.Empty;
        }

        GroupByColumn();
    }
    #endregion

    protected void ResetGroupBy()
    {
        TreeToRender = new();
        this.GroupByProprties = new();
        this.ParentComponent.ParentComponent.PropertiesRendered = this.ParentComponent?.ParentComponent?.InitPropertiesRendered() ?? new();
        this.ParentComponent.ParentComponent.MergedCells = this.ParentComponent?.ParentComponent?.InitMergedCells() ?? new();
        ResetRenderedProperties();
        this.ParentComponent.ParentComponent.TItemsChangedEventInvoke(this.ParentComponent.ParentComponent.ItemsFiltered);
    }

    public void GroupByColumn(PropertyInfo prop = null, bool isNeedInvokeEvent = true)
    {
        TreeToRender = new();
        IsGroupByZoneRender = true;

        // Обновить коллекцию ключей, по которым будет вестись группировка
        if (prop != null && (!(GroupByProprties?.Contains(prop) ?? true)))
        {
            GroupByProprties?.AddLast(prop);
        }

        // Обновить дерево
        TreeToRender = BuildTree();

        // СКРЫТО, ПОТОМУ ЧТО НЕ НУЖНО, ЧТОБЫ ПРИ ГРУППИРОВКЕ СТОЛБЕЦ ИСЧЕЗАЛ ИЗ ОТОБРАЖАЕМЫХ В ТЕЛЕ ТАБЛИЦЫ
        //// Обновить коллекцию колонок в таблице - скрыть ту, по которой запущена группировка
        //var propsToRender = ParentComponent?.ParentComponent?.PropertiesRendered?.Where(_ => !GroupByProprties.Any(o => o == _))?.ToList() ?? new();
        //this.ParentComponent.ParentComponent.PropertiesRendered = propsToRender;

        //// Исключить столбец из обьединенных в заголовке таблицы столбцов
        //if (this.ParentComponent.ParentComponent.MergedCells?.Values?.Any(_ => _.Any(o => o == prop)) ?? false)
        //{
        //    Dictionary<string, List<PropertyInfo>> mergedCells = new();

        //    foreach (var pare in this.ParentComponent?.ParentComponent?.MergedCells ?? new())
        //    {
        //        mergedCells.Add(pare.Key, pare.Value?.Where(_ => _ != prop)?.ToList() ?? new());
        //    }

        //    this.ParentComponent.ParentComponent.MergedCells = mergedCells;
        //}

        if(isNeedInvokeEvent)
            this.ParentComponent.ParentComponent.TItemsChangedEventInvoke(this.ParentComponent.ParentComponent.ItemsFiltered);
    }

    private List<Func<T, (string name, object value)>> GetGroupByKeys()
    {
        List<Func<T, (string name, object value)>> result = new();
        foreach (var groupByProprty in GroupByProprties)
        {
            var resultItem = (T item) => (groupByProprty.Name, groupByProprty.GetValue(item));

            result.Add(resultItem);
        }

        return result;
    }

    private Tree<(object propName, object propValue, List<T> itemsInGroup)> BuildTree()
    {
        Tree<(object propName, object propValue, List<T> itemsInGroup)> tree = new();

        var toGroup = ((Func<T, (string name, object value)> keyValue, List<T> itemsValue) source) =>
        {
            List<(object propName, object propValue, List<T> itemsInGroup)> result = new();
            var groupedItems = source.itemsValue?.GroupBy(_ => source.keyValue(_))?.ToList() ?? new();
            groupedItems.ForEach(groupedItem => result.Add(
                (typeof(T).GetCustomDisplayValues(groupedItem.Key.name).Item1 ?? string.Empty, groupedItem.Key.value, groupedItem.ToList())));
            return result;
        };

        var level = 0;
        TreeNode<(object propName, object propValue, List<T> itemsInGroup)> parent = null;
        List<TreeNode<(object propName, object propValue, List<T> itemsInGroup)>> rootNodes = new();
        var kees = GetGroupByKeys();

        if (!kees.Any()) return default;

        // add to tree root nodes
        var currentKey = kees.ElementAt(level);
        var groupedItems = toGroup((currentKey, ParentComponent?.ParentComponent?.ItemsFiltered ?? new()));
        foreach (var groupedItem in groupedItems)
        {
            rootNodes.Add(new((groupedItem.propName, groupedItem.propValue, groupedItem.itemsInGroup), 0, parent));
        }
        tree.Nodes.AddRange(rootNodes);
        level++;

        // add to tree child nodes
        var currentNodes = rootNodes;
        while (level < kees.Count)
        {
            currentKey = kees.ElementAt(level);
            List<TreeNode<(object propName, object propValue, List<T> itemsInGroup)>> childNodes = new();

            foreach (var currentNode in currentNodes)
            {
                parent = currentNode;
                groupedItems = toGroup((currentKey, currentNode.Value.itemsInGroup));
                foreach (var groupedItem in groupedItems)
                {
                    var childNode = currentNode.AddChild((groupedItem.propName, groupedItem.propValue, groupedItem.itemsInGroup));
                    childNodes.Add(childNode);
                }
            }

            level++;
            currentNodes = childNodes;
        }

        return tree;
    }
}

//////////////////////////////////////////////////////////////////////////////////TREE/////////////////////////////////////////////////////////////////////////////////
public class Tree<TData>
{
    public List<TreeNode<TData>> Nodes { get; } = new();
}

public class TreeNode<TData>
{
    public TData Value { get; }
    public int Level { get; internal set; }
    public TreeNode<TData> Parent { get; }
    public List<TreeNode<TData>> Children { get; } = new();
    public bool IsLeaf => !Children.Any();
    public bool IsRoot => Parent == null;

    public TreeNode(TData val, int level, TreeNode<TData> parent)
    {
        Value = val;
        Level = level;
        Parent = parent;
    }

    public TreeNode<TData> AddChild(TData val)
    {
        var node = new TreeNode<TData>(val, this.Level + 1, this);
        Children.Add(node);
        return node;
    }
}