using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace TestMobit.Domain.Interfaces.Repositories.Database
{
    public interface IDatabaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(FindOptions? findOptions = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        Task Add(TEntity entity);
        Task AddMany(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
