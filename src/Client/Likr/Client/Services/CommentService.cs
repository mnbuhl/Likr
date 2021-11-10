using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class CommentService : ICommentService
{
    private readonly IHttpService _httpService;
    private const string Endpoint = "v1/c/Comments";

    public CommentService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<List<CommentDto>> GetComments()
    {
        var wrapper = await _httpService.Get<List<CommentDto>>($"{Endpoint}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<List<CommentDto>> GetCommentsByPostId(Guid postId)
    {
        var wrapper = await _httpService.Get<List<CommentDto>>($"{Endpoint}/post/{postId}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public async Task<CommentDto> GetCommentById(Guid id)
    {
        var wrapper = await _httpService.Get<CommentDto>($"{Endpoint}/post/{id}");

        return wrapper.Response ?? throw new HttpRequestException(wrapper.HttpResponseMessage.ReasonPhrase);
    }

    public Task<CommentDto> CreateComment(CreateCommentDto commentDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteComment(Guid id)
    {
        throw new NotImplementedException();
    }
}