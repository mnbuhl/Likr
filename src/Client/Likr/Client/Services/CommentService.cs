using Likr.Client.Dtos;

namespace Likr.Client.Services;

public class CommentService : ICommentService
{
    private readonly IHttpService _httpService;
    private const string Endpoint = "v1/p/Comments";

    public CommentService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public Task<List<CommentDto>> GetComments()
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentDto>> GetCommentsByPostId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDto> GetCommentById(Guid id)
    {
        throw new NotImplementedException();
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