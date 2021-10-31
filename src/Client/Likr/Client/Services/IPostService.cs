using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface IPostService
{
    public Task<List<PostDto>> GetPosts(int pageSize, int page);
    public Task<List<PostDto>> GetPostsByUserId(Guid userId);
    public Task<PostDto> GetById(Guid id);
    public Task<PostDto> CreatePost(CreatePostDto postDto);
    public Task DeletePost(Guid id);
}