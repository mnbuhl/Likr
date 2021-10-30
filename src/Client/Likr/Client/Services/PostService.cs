using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface IPostService
{
    public Task<List<PostDto>> GetPosts();
    public Task<List<PostDto>> GetPostsByUserId(Guid userId);
}