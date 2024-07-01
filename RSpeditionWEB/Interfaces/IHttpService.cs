namespace RSpeditionWEB.Interfaces;

public interface IHttpService : INotify
{
    Task<TResponse> SendRequestAsync<TRequest, TResponse>(
        string url,
        HttpMethod httpMethod,
        TRequest content,
        CancellationToken token = default);

    Task<TResponse> SendRequestAsync<TResponse>(
        string url,
        HttpMethod httpMethod,
        CancellationToken token = default);
}
