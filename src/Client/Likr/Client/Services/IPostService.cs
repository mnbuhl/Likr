using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface IPostService
{
    public Task<List<PostDto>> GetPosts(int pageSize = 10, int page = 1);
    public Task<List<PostDto>> GetPostsByUserId(Guid userId, int pageSize = 10, int page = 1);
    public Task<List<PostDto>> GetPostsByUsername(string username, int pageSize = 10, int page = 1);
    public Task<PostDto> GetById(Guid id);
    public Task<PostDto> CreatePost(CreatePostDto postDto);
    public Task<bool> DeletePost(Guid id);
}