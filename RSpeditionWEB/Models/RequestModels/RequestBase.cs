namespace RSpeditionWEB.Models.RequestModels;

public abstract class RequestBase
{
    public RequestBase()
    {
        UserName = string.Empty;
    }

    public RequestBase(string userName)
    {
        UserName = userName;
    }

    [JsonInclude]
    public string UserName { get; set; }
}