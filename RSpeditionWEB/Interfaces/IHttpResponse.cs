namespace RSpeditionWEB.Interfaces
{
    public interface IHttpResponse
    {
        string Result { get; }
        HttpStatusCode StatusCode { get; }
    }
}
