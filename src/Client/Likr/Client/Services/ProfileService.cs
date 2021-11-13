using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class ProfileService : IProfileService
{
    private readonly IHttpService _httpService;
    private const string Endpoint = "v1/i/Users";

    public ProfileService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<UserDto?> GetProfileByUsername(string username)
    {
        var wrapper = await _httpService.Get<UserDto>(Endpoint + "/" + username);
        return wrapper.Response;
    }
}