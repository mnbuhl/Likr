using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class PostService : IPostService
{
    private readonly IHttpService _httpService;

    public PostService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<List<PostDto>> GetPosts(int pageSize = 10, int page = 1)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"/api/v1/p/Posts?pageSize={pageSize}?page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public Task<List<PostDto>> GetPostsByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PostDto> CreatePost(CreatePostDto postDto)
    {
        throw new NotImplementedException();
    }

    public Task DeletePost(Guid id)
    {
        throw new NotImplementedException();
    }
}