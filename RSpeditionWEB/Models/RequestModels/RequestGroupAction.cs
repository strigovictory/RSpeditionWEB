namespace RSpeditionWEB.Models.RequestModels;

public class RequestGroupAction<T> : RequestBase
{
    public RequestGroupAction(string userName, List<T> items)
        : base(userName)
    {
        Items = items;
    }

    /// <summary>
    /// Коллекция экземпляров, над которыми нужно произвести групповую операцию.
    /// </summary>
    [JsonInclude]
    public List<T> Items { get; set; }
}