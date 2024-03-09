using Luftborn.Domain.Common;
using System.Linq.Expressions;

namespace Luftborn.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        string? includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true, int skipRowsCount = 0, int takeRowsCount = 10);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<TType>> SelectAsync<TType>(Expression<Func<T, TType>> select, bool isDistinct = false);
        Task<IReadOnlyList<TType>> SelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select, bool isDistinct = false);
    }
}
