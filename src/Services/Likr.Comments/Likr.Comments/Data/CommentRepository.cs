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
            return await session.Query<Comment>().Include("Comments").Where(x => x.PostId == postId, true).ToListAsync();
        }

        public async Task<Comment> Get(string id)
        {
            using var session = _context.Store.OpenAsyncSession();
            var comment = await session.Include("Comments").LoadAsync<Comment>(id);

            return comment;
        }

        // public async Task<bool> InsertOrUpdate(Comment comment)
        // {
        //     using var session = _context.Store.OpenAsyncSession();
        //
        //     try
        //     {
        //         comment.Id = Guid.NewGuid().ToString();
        //         comment.User = await session.Query<User>().FirstOrDefaultAsync(x => x.Id == comment.UserId);
        //
        //         var existingComment = await session.Query<Comment>().Include(x => x.Comments)
        //             .FirstOrDefaultAsync(x => x.Id == comment.PostId);
        //
        //         if (existingComment != null)
        //         {
        //             if (existingComment.Comments is not null)
        //             {
        //                 existingComment.Comments.Add(comment);
        //             }
        //             else
        //             {
        //                 existingComment.Comments = new List<Comment> { comment };
        //             }
        //
        //             await session.StoreAsync(existingComment, existingComment.Id);
        //         }
        //         else
        //         {
        //             await session.StoreAsync(comment, comment.Id);
        //         }
        //
        //         await session.SaveChangesAsync();
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError(e, "Failed to store comment in database");
        //         return false;
        //     }
        //
        //     return true;
        // }

        public async Task<bool> InsertOrUpdate(Comment comment)
        {
            using var session = _context.Store.OpenAsyncSession();

            var existingComment = await session.Include("Comments").LoadAsync<Comment>(comment.PostId);
            var user = await session.LoadAsync<User>(comment.UserId);
            
            comment.Id = Guid.NewGuid().ToString();
            comment.User = user;

            try
            {
                if (existingComment == null)
                {
                    await session.StoreAsync(comment, comment.Id);
                }
                else
                {
                    
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
            var comment = await session.LoadAsync<Comment>(id.ToString());

            if (comment == null)
                return false;

            session.Delete(comment);
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