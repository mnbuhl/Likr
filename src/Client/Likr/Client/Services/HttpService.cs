using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Likr.Client.Helpers;

namespace Likr.Client.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _client;

    public HttpService(HttpClient client, IConfiguration configuration)
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("GatewayUri") + "/api/");
        _client = client;
    }

    public async Task<HttpResponseWrapper<T?>> Get<T>(string url, string token = "")
    {
        SetAuthorizationHeader(token);

        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new HttpResponseWrapper<T?>(false, default, response);

        var responseDeserialized = await Deserialize<T>(response);

        return new HttpResponseWrapper<T?>(true, responseDeserialized, response);
    }

    public async Task<HttpResponseWrapper<TResponse?>> Create<T, TResponse>(string url, T data, string token = "")
    {
        SetAuthorizationHeader(token);

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

    public async Task<HttpResponseWrapper<object>> Update<T>(string url, T data, string token = "")
    {
        SetAuthorizationHeader(token);

        string dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(url, stringContent);

        if (!response.IsSuccessStatusCode)
        {
            return new HttpResponseWrapper<object>(false, await response.Content.ReadAsStringAsync(), response);
        }

        return new HttpResponseWrapper<object>(true, response.IsSuccessStatusCode, response);
    }

    public async Task<HttpResponseWrapper<object?>> Delete(string url, string token = "")
    {
        SetAuthorizationHeader(token);

        var response = await _client.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
            return new HttpResponseWrapper<object?>(false, default, response);

        return new HttpResponseWrapper<object?>(true, response.IsSuccessStatusCode, response);
    }

    private static async Task<T?> Deserialize<T>(HttpResponseMessage httpResponse)
    {
        var serializerOptions = new JsonSerializerOptions
        { PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.Preserve };
        string response = await httpResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(response, serializerOptions);
    }

    private void SetAuthorizationHeader(string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}