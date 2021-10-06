using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Likr.Posts.Entities;

namespace Likr.Posts.Interfaces
{
    public interface IPostRepository
    {
        Task<IList<Post>> GetAllPosts();
        Task<IList<Post>> GetPostsByUserId(string userId);
        Task<Post> GetPost(Guid id);
        Task<bool> CreatePost(Post post);
        Task<bool> DeletePost(Guid id);
    }
}