using System;
using System.Collections.Generic;
using System.Linq;
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
            return await session.Query<Comment>().Include("Comments").ToListAsync();
        }

        public async Task<IList<Comment>> GetAllByPostId(string postId)
        {
            using var session = _context.Store.OpenAsyncSession();
            return await session.Query<Comment>().Include("Comments").Where(x => x.PostId == postId, true)
                .ToListAsync();
        }

        public async Task<Comment> Get(string id)
        {
            using var session = _context.Store.OpenAsyncSession();
            var comment = await session.Include("Comments").LoadAsync<Comment>(id);

            if (comment != null)
                return comment;

            var nestedComment = await session.Query<Comment>().Include("Comments")
                .Where(x => x.Comments.Any(y => y.Id == id))
                .FirstOrDefaultAsync();

            return nestedComment;
        }

        public async Task<bool> InsertOrUpdate(Comment comment)
        {
            using var session = _context.Store.OpenAsyncSession();

            var existingComment = await session.Include("Comments").LoadAsync<Comment>(comment.PostId);
            var user = await session.LoadAsync<User>(comment.UserId);

            comment.Id = Guid.NewGuid().ToString();
            comment.User = user;

            try
            {
                if (existingComment == null && comment.Comments == null)
                {
                    Console.WriteLine("HIT HERE 1st szkjhdaskjdkjhasdkjh");
                    await session.StoreAsync(comment);
                }
                else if (existingComment != null && !existingComment.Comments.Any())
                {
                    existingComment.Comments ??= new List<Comment> { comment };
                    await session.StoreAsync(existingComment);
                }
                else if (existingComment != null && existingComment.Comments.Any())
                {
                    existingComment.Comments.Add(comment);
                    await session.StoreAsync(existingComment);
                }

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
            var comment = await session.Include("Comments").LoadAsync<Comment>(id.ToString());

            if (comment != null)
            {
                session.Delete(comment);
                await session.SaveChangesAsync();
                return true;
            }

            var nestedComment = await session.Query<Comment>().Include("Comments")
                .Where(x => x.Comments.Any(y => y.Id == id.ToString()))
                .FirstOrDefaultAsync();

            if (nestedComment == null)
                return false;

            nestedComment.Comments.Remove(nestedComment.Comments.FirstOrDefault(x => x.Id == id.ToString()));
            await session.StoreAsync(nestedComment);

            await session.SaveChangesAsync();
            return true;
        }

        public async Task Delete(Comment comment)
        {
            using var session = _context.Store.OpenAsyncSession();
            session.Delete(comment);
            await session.SaveChangesAsync();
        }
    }
}