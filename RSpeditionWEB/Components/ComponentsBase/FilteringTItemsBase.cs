namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class FilteringTItemsBase<T> : PrintBase<T>
    {
        public T ItemSelected { get; set; }

        [Parameter]
        public EventCallback<T> SendToParentSelectedItem { get; set; }

        [Parameter]
        public EventCallback<CoordinateClass<T>> SendToParentSelectedItemRightClick { get; set; }

        public delegate void TItemsChangedDelegate(List<T> items); // делегат события изменения коллекции

        public event TItemsChangedDelegate TItemsChangedEvent; // событие изменения коллекции

        public void TItemsChangedEventInvoke(List<T> items) => this.TItemsChangedEvent?.Invoke(items ?? new()); // запуск события изменения коллекции

        // Событие изменения коллекции отмеченных галочками экземпляров
        protected event TItemsChangedDelegate CheckedCollectionChangedEvent;

        // Метод, запускающий событие изменения коллекции экземпляров, отмеченных/выделенных галочками
        public void CheckedCollectionChangedEventInvoke(List<T> items) => this.CheckedCollectionChangedEvent?.Invoke(items ?? new());
    }
}