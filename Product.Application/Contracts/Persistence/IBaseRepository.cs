using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Persistence
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void Add(T entity);

    }
}
