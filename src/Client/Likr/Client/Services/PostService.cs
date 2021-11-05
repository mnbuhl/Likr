using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class PostService : IPostService
{
    private readonly IHttpService _httpService;

    public PostService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<List<PostDto>> GetPosts(int pageSize, int page)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"v1/p/Posts?pageSize={pageSize}&page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<List<PostDto>> GetPostsByUserId(Guid userId, int pageSize, int page)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"v1/p/Posts/user/{userId}?pageSize={pageSize}&page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<PostDto> GetById(Guid id)
    {
        var wrapper = await _httpService.Get<PostDto>($"v1/p/Posts/{id}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<PostDto> CreatePost(CreatePostDto postDto, string token)
    {
        var wrapper = await _httpService.Create<CreatePostDto, PostDto>("v1/p/Posts", postDto);
        
        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public Task DeletePost(Guid id)
    {
        throw new NotImplementedException();
    }
}