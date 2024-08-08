using Domain.Common.Entities;
using Domain.Common.PagedList;
using Domain.Common.Persistence;
using Domain.Common.Queries.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Formats.Asn1;
using System.Linq.Expressions;

namespace Infrastructure.Common.Repository
{
    public partial class RepositoryBase<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityBase<string>
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query => _dbSet;

        public IQueryable<TEntity> QueryAsNoTracking => _dbSet.AsNoTracking();

        protected IProperty GetPrimaryKeyProperty()
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            if (entityType == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' is not part of the model for the current context.");

            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null) throw new InvalidOperationException($"The type '{typeof(TEntity).Name}' does not have a primary key defined.");

            var keyProperty = primaryKey.Properties.Single();

            return keyProperty;
        }
    }
}
