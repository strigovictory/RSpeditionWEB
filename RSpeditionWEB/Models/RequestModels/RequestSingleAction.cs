namespace RSpeditionWEB.Models.RequestModels;

public class RequestSingleAction<T> : RequestBase
{
    public RequestSingleAction(string userName, T item)
        : base(userName)
    {
        Item = item;
    }

    /// <summary>
    /// Экземпляр, над которым нужно произвести операцию.
    /// </summary>
    [JsonInclude]
    public T Item { get; set; }
}