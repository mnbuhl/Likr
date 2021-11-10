using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface ICommentService
{
    public Task<List<CommentDto>> GetComments();
    public Task<List<CommentDto>> GetCommentsByPostId(Guid userId);
    public Task<CommentDto> GetCommentById(Guid id);
    public Task<CommentDto> CreateComment(CreateCommentDto commentDto);
    public Task DeleteComment(Guid id);
}