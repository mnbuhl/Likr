using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Likr.Client.Extensions;
using Likr.Client.Helpers;

namespace Likr.Client.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _client;
    private readonly HttpClient _clientNoAuth;

    public HttpService(IHttpClientFactory clientFactory)
    {
        _clientNoAuth = clientFactory.CreateClient("GatewayApi.NoAuth");
        _client = clientFactory.CreateClient("GatewayApi.Auth");
    }

    public async Task<HttpResponseWrapper<T?>> Get<T>(string url)
    {
        var response = await _clientNoAuth.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new HttpResponseWrapper<T?>(false, default, response);

        var responseDeserialized = await Deserialize<T?>(response);

        return new HttpResponseWrapper<T?>(true, responseDeserialized, response);
    }

    public async Task<HttpResponseWrapper<TResponse?>> Create<T, TResponse>(string url, T data)
    {
        string dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            return new HttpResponseWrapper<TResponse?>(false, default, response);
        }

        var responseDeserialized = await Deserialize<TResponse>(response);
        return new HttpResponseWrapper<TResponse?>(true, responseDeserialized, response);
    }

    public async Task<HttpResponseWrapper<object>> Update<T>(string url, T data)
    {
        string dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(url, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            return new HttpResponseWrapper<object>(false, await response.Content.ReadAsStringAsync(), response);
        }

        return new HttpResponseWrapper<object>(true, response.IsSuccessStatusCode, response);
    }

    public async Task<HttpResponseWrapper<object?>> Delete(string url)
    {
        var response = await _client.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
            return new HttpResponseWrapper<object?>(false, default, response);

        return new HttpResponseWrapper<object?>(true, response.IsSuccessStatusCode, response);
    }

    private static async Task<T?> Deserialize<T>(HttpResponseMessage httpResponse)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, 
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        return await httpResponse.Content.ReadFromJsonAsync<T?>(serializerOptions);
    }
}