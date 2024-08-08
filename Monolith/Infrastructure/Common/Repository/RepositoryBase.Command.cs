using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Repository
{
    internal partial class RepositoryBase<TEntity>
    {
        public virtual async Task<TEntity?> FindAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task AddAsync(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || entities.Any())
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _dbSet.UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var entitiesToDelete = _dbContext.Set<TEntity>().Where(e => ids.Contains(EF.Property<object>(e, "Id")));

            await entitiesToDelete.ExecuteDeleteAsync(cancellationToken);
        }

    }
}
