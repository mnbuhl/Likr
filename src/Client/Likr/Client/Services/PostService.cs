using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class PostService : IPostService
{
    private readonly IHttpService _httpService;
    private const string Endpoint = "v1/p/Posts";

    public PostService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<List<PostDto>> GetPosts(int pageSize, int page)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"{Endpoint}?pageSize={pageSize}&page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<List<PostDto>> GetPostsByUserId(Guid userId, int pageSize, int page)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"{Endpoint}/user/{userId}?pageSize={pageSize}&page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<List<PostDto>> GetPostsByUsername(string username, int pageSize = 10, int page = 1)
    {
        var wrapper = await _httpService.Get<List<PostDto>>($"{Endpoint}/username/{username}?pageSize={pageSize}&page={page}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<PostDto> GetById(Guid id)
    {
        var wrapper = await _httpService.Get<PostDto>($"{Endpoint}/{id}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<PostDto> CreatePost(CreatePostDto postDto)
    {
        var wrapper = await _httpService.Create<CreatePostDto, PostDto>(Endpoint, postDto);

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<bool> DeletePost(Guid id)
    {
        var wrapper = await _httpService.Delete($"{Endpoint}/{id}");

        return wrapper.Success;
    }
}