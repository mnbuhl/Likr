using System.Collections.Generic;
using System.Threading.Tasks;
using Likr.Comments.Entities;
using Likr.Comments.Interfaces;

namespace Likr.Comments.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRavenDbStore _store;

        public CommentRepository(IRavenDbStore store)
        {
            _store = store;
        }

        public Task GetAll()
        {
            return Task.CompletedTask;
        }
    }
}