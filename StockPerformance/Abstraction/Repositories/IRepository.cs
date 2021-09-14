using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Abstraction.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(T entity);
        Task<int> AddBatchAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetOnConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
