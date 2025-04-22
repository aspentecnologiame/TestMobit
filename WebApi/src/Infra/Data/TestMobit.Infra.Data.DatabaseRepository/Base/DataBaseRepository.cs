using System.Linq.Expressions;
using TestMobit.Domain;
using Microsoft.EntityFrameworkCore;
using TestMobit.Domain.Interfaces.Repositories.Database;

namespace TestMobit.Infra.Data.DatabaseRepository.Base
{
    public class DataBaseRepository<TEntity> : IDatabaseRepository<TEntity> where TEntity : class
    {
        private readonly DatabaseContext _databaseContext;
        public DataBaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task Add(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Add(entity);
            await Task.FromResult(_databaseContext.SaveChanges());
        }
        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            _databaseContext.Set<TEntity>().AddRange(entities);
            await Task.FromResult(_databaseContext.SaveChanges());
        }
        public async Task Delete(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity);
            await Task.FromResult(_databaseContext.SaveChanges());
        }
        public async Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Find(predicate);
            _databaseContext.Set<TEntity>().RemoveRange(entities);
            await Task.FromResult(_databaseContext.SaveChanges());
        }
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }
        public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }
        public async Task Update(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Update(entity);
            await Task.FromResult(_databaseContext.SaveChanges());
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _databaseContext.Set<TEntity>().Any(predicate);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _databaseContext.Set<TEntity>().Count(predicate);
        }
        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _databaseContext.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            return entity;
        }
    }
}
