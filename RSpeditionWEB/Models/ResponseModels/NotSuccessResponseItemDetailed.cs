namespace RSpeditionWEB.Models.Results;

public class NotSuccessResponseItemDetailed<T>
{
    public NotSuccessResponseItemDetailed() { }

    public NotSuccessResponseItemDetailed(T item, string reason)
    {
        this.NotSuccessItem = item;
        this.Reason = reason;
    }

    [JsonInclude]
    [JsonPropertyName(nameof(NotSuccessItem))]
    public T NotSuccessItem { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Reason))]
    public string Reason { get; set; }
}
