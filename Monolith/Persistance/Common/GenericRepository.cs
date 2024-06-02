using Domain.Common.Entities;
using Domain.Common.PagedList;
using Domain.Common.Persistence;
using Domain.Common.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Common
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity?> FindAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : class
        {
            return await _dbSet.WhereWithExist()
                                .SelectWithField<TEntity, TResult>()
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task<bool> IsExistAsync(params int[] ids)
        {
            string stringIds = string.Join(",", ids);
            return await _dbSet
                .AsNoTracking()
                .WhereByStringWithExist($"e.{EntityFrameworlExtensions.GetPrimaryKeyName<TEntity>()} IN ({stringIds})")
                .AnyAsync();
        }

        public abstract Task<IPagedList<TResult>> SearchAsync<TQuery, TResult>(in TQuery query)
                  where TQuery : IPagingQuery<TResult>
                  where TResult : class;

        public async Task CreateAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            var condition = $"e.{EntityFrameworlExtensions.GetPrimaryKeyName<TEntity>()} IN ({string.Join(",", ids)})";
            var entityDelete = await _dbSet.WhereByStringWithExist(condition).ToListAsync();

            foreach (var entity in entityDelete)
            {
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            }

            _dbContext.UpdateRange(entityDelete);
        }
    }
}
