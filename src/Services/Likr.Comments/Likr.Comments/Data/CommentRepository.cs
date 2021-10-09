using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Likr.Comments.Entities;
using Likr.Comments.Interfaces;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;

namespace Likr.Comments.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRavenDbStore _context;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(IRavenDbStore context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<Comment>> GetAll()
        {
            using var session = _context.Store.OpenAsyncSession();
            return await session.Query<Comment>().ToListAsync();
        }

        public async Task<IList<Comment>> GetAllByPostId(Guid postId)
        {
            using var session = _context.Store.OpenAsyncSession();
            return await session.Query<Comment>().Where(x => x.PostId == postId, true).ToListAsync();
        }

        public async Task<Comment> Get(string id)
        {
            using var session = _context.Store.OpenAsyncSession();
            var comment = await session.LoadAsync<Comment>(id);
            comment.Comments = await GetAllByPostId(Guid.Parse(comment.Id));

            return comment;
        }

        public async Task<bool> Insert(Comment comment)
        {
            using var session = _context.Store.OpenAsyncSession();

            try
            {
                await session.StoreAsync(comment, Guid.NewGuid().ToString());
                await session.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to store comment in database");
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            using var session = _context.Store.OpenAsyncSession();
            var comment = await session.LoadAsync<Comment>(id.ToString());

            if (comment == null)
                return false;
            
            session.Delete(comment);
            await session.SaveChangesAsync();
            return true;
        }
    }
}