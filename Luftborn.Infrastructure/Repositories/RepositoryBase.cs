using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Domain.Common;
using Luftborn.Infrastructure.Persistence;

namespace Luftborn.Infrastructure.Repositories
{
    public class RepositoryBase<T> :IAsyncRepository<T> where T : EntityBase
    {
        protected readonly LuftbornContext _luftbornContext;
        protected DbSet<T> DbSet => _luftbornContext.Set<T>();

        public RepositoryBase(LuftbornContext installmentContext)
        {
            _luftbornContext = installmentContext ?? throw new ArgumentNullException(nameof(installmentContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);

            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true, int skipRowsCount = 0, int takeRowsCount = 10)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            query = query.Skip(skipRowsCount).Take(takeRowsCount);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            DbSet.Add(entity);
            await _luftbornContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _luftbornContext.Entry(entity).State = EntityState.Modified;
            await _luftbornContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await _luftbornContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<TType>> SelectAsync<TType>(Expression<Func<T, TType>> select, bool isDistinct = false)
        {
            if (isDistinct)
            {
                return await DbSet.Select(select).Distinct().ToListAsync();
            }
            else
            {
                return await DbSet.Select(select).ToListAsync();
            }
        }
        public async Task<IReadOnlyList<TType>> SelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select, bool isDistinct = false)
        {
            if (isDistinct)
            {
                return await DbSet.Where(where).Select(select).Distinct().ToListAsync();
            }
            else
            {
                return await DbSet.Where(where).Select(select).ToListAsync();
            }
        }
    }
}
