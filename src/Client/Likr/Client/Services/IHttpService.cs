using Likr.Client.Helpers;

namespace Likr.Client.Services;

public interface IHttpService
{
    Task<HttpResponseWrapper<T?>> Get<T>(string url);
    Task<HttpResponseWrapper<TResponse?>> Create<T, TResponse>(string url, T data);
    Task<HttpResponseWrapper<object>> Update<T>(string url, T data);
    Task<HttpResponseWrapper<object?>> Delete(string url);
}