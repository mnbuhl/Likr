using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class LikeService : ILikeService
{
    private readonly IHttpService _httpService;
    private const string Endpoint = "v1/l/Likes";

    public LikeService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<IList<LikeDto>?> GetLikesByPostId(string postId)
    {
        var wrapper = await _httpService.Get<IList<LikeDto>>($"{Endpoint}/posts/{postId}");

        return wrapper.Response;
    }

    public async Task<IList<LikeDto>?> GetLikesByUserId(string userId)
    {
        var wrapper = await _httpService.Get<IList<LikeDto>>($"{Endpoint}/users/{userId}");

        return wrapper.Response;
    }

    public async Task<bool> Like(CreateLikeDto createLike)
    {
        var wrapper = await _httpService.Create<CreateLikeDto, object>(Endpoint, createLike);

        return wrapper.Success;
    }

    public async Task<bool> Unlike(string postId)
    {
        var wrapper = await _httpService.Create<string, object>(Endpoint, postId);

        return wrapper.Success;
    }
}