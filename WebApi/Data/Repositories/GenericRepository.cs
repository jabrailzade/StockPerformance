using Core.Abstraction.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly DataContext _dataContext;

        public GenericRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<Guid> AddAsync(T entity)
        {
            _dataContext.Set<T>().Add(entity);
            await _dataContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> AddBatchAsync(IEnumerable<T> entities)
        {
            _dataContext.Set<T>().AddRange(entities);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dataContext
                .Set<T>()
                .AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dataContext
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetOnConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dataContext.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dataContext.Set<T>().Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
