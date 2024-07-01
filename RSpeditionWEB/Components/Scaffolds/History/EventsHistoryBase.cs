namespace RSpeditionWEB.Components.Scaffolds.History
{
    public abstract class EventsHistoryBase<T, V> : ScaffoldJCFiltersBase<V>
    {
        public List<T> EventHistoryItems { get; set; }
        protected T EventHistorySelected { get; set; }
        protected bool Show_AddEventHistory { get; set; }
        protected bool Show_DeleteEventHistory { get; set; }
        protected bool Show_UpdateEventHistory { get; set; }

        protected void ActionsChoice((OperationsType operType, T item) actionToDo)
        {
            EventHistorySelected = actionToDo.item;

            switch (actionToDo.operType)
            {
                case OperationsType.create:
                    {
                        Show_AddEventHistory = true;
                        break;
                    }
                case OperationsType.update:
                    {
                        Show_UpdateEventHistory = true;
                        break;
                    }
                case OperationsType.delete:
                    {
                        Show_DeleteEventHistory = true;
                        break;
                    }
            }
        }
    }
}
