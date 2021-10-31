using Likr.Client.Helpers;

namespace Likr.Client.Services;

public interface IHttpService
{
    Task<HttpResponseWrapper<T?>> Get<T>(string url, string token = "");
    Task<HttpResponseWrapper<TResponse?>> Create<T, TResponse>(string url, T data, string token = "");
    Task<HttpResponseWrapper<object>> Update<T>(string url, T data, string token = "");
    Task<HttpResponseWrapper<object?>> Delete(string url, string token = "");
}