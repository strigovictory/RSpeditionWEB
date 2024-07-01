namespace RSpeditionWEB.Interfaces
{
    public delegate Task AsyncStateDelegate();
    public delegate void StateDelegate();
    public delegate void StateCollectionDelegate<T>(List<T> items);
    public delegate void StateItemsDelegate<T>(T items);

    public interface IStateComponentWereChangedAsync<T>
    {
        public event AsyncStateDelegate StateComponentWereChangedAsync;
    }

    public interface IStateComponentWereChanged<T>
    {
        public event StateDelegate StateComponentWereChanged;
    }

    public interface ICollectionComponentWereChanged<T>
    {
        public event StateCollectionDelegate<T> StateCollectionComponentWereChanged;
    }
    public interface IStateItemsWereChanged<T>
    {
        public T Items {get;}
        public event StateItemsDelegate<T> StateItemsWereChangedEvent;
    }
}
