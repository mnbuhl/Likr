using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Likr.Likes.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> criteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria = null);
    }
}