using Likr.Client.Dtos;

namespace Client.Services
{
    public interface PostService
    {
        public Task<List<PostDto>> GetPosts();
        public Task<List<PostDto>> GetPostsByUserId(Guid userId);
    }
}