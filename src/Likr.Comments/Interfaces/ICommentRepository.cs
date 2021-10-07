using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Likr.Comments.Entities;

namespace Likr.Comments.Interfaces
{
    public interface ICommentRepository
    {
        Task<IList<Comment>> GetAll();
        Task<IList<Comment>> GetAllByPostId(Guid postId);
        Task<Comment> Get(string id);
        Task<bool> Insert(Comment comment);
        Task<bool> Delete(Guid id);
    }
}