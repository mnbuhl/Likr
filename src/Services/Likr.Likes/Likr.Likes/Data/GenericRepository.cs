using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Likr.Likes.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Likr.Likes.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // Takes an expression as the criteria such is Id and an expression to include navigation properties 
        // Builds a query and gets the First item that matches the criteria
        public async Task<T> GetAsync(Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(criteria);
        }

        // Takes an expression as the criteria such is a Where clause, an expression to include navigation properties and an expression for OrderBy
        // Builds a query and returns the list that matches the criteria
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> criteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            return await query.AsNoTracking().ToListAsync();
        }

        // Takes a generic entity and adds it to the database
        // Returns true if successfully saved to database
        public async Task<bool> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        // Takes a generic entity and detaches it and then updates it
        // Returns true if successfully updated in the database
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        // Takes an id and will try to find an entity from that Id
        // Returns true if successfully deleted
        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            return await query.CountAsync();
        }
    }
}