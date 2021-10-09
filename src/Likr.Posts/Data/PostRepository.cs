using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Likr.Posts.Entities;
using Likr.Posts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Post>> GetAllPosts()
        {
            List<Post> posts = await _context.Posts.ToListAsync();

            return posts;
        }

        public async Task<IList<Post>> GetPostsByUserId(string userId)
        {
            List<Post> posts = await _context.Posts.Where(x => x.UserId == userId).ToListAsync();

            return posts;
        }

        public async Task<Post> GetPost(Guid id)
        {
            return await _context.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreatePost(Post post)
        {
            _context.Posts.Add(post);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePost(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post is null)
                return false;

            _context.Posts.Remove(post);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}