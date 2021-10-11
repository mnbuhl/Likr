using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Likr.Comments.Entities;

namespace Likr.Comments.Interfaces
{
    public interface ICommentRepository
    {
        Task<IList<Comment>> GetAll();
        Task<IList<Comment>> GetAllByPostId(string postId);
        Task<Comment> Get(string id);
        Task<bool> InsertOrUpdate(Comment comment);
        Task<bool> Delete(Guid id);
    }
}