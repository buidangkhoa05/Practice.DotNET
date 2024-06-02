using Domain.Common.Entities;
using Domain.Common.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.DbContexts;
using System.Reflection;

namespace Persistence.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repoCache;
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction? transaction = null;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repoCache = new Dictionary<string, object>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (transaction != null)
                throw new InvalidOperationException("Transaction has already been started.");

            transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (transaction == null)
                    throw new InvalidOperationException("Transaction has not been started.");

                await transaction.CommitAsync(cancellationToken);

                transaction = null;
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        private async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await transaction.RollbackAsync(cancellationToken);

            transaction = null;
        }

        public TRepostitoy Instance<TRepostitoy>(Type? entityType = null)
           where TRepostitoy : IRepositoryBase
        {
            entityType ??= typeof(TRepostitoy).GetInterface("IGenericRepository`1")?.GenericTypeArguments.FirstOrDefault();

            if (entityType?.GetInterface(nameof(IEntityBase)) != null) // entity is derived from IEntity
            {
                return SetOrGetRepository<TRepostitoy>(entityType.Name);
            }

            throw new ArgumentException("Entity must be derived from IEntity");
        }

        private Repository SetOrGetRepository<Repository>(string entityName)
           where Repository : IRepositoryBase
        {
            var instance = _repoCache.GetValueOrDefault(entityName);

            if (instance != null && instance is Repository repository)
                return repository;

            var classRepository = GetClassImplementingInterface(typeof(Repository));

            instance = Activator.CreateInstance(classRepository, _dbContext);

            ArgumentNullException.ThrowIfNull(instance, $"Cannot create instance of {classRepository.Name}");

            repository = (Repository)instance;

            _repoCache.Add(entityName, repository);

            return repository;
        }

        private Type GetClassImplementingInterface(Type interfaceType)
        {
            var genericType = interfaceType.GenericTypeArguments.FirstOrDefault();

            Type? type = null;
            if (genericType != null)
            {
                type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t =>
                        t.IsClass == true
                        && t.IsAbstract == false
                        && (t.GetInterface(interfaceType.Name)?.GetGenericArguments()?.Any(a => a.Name == genericType.Name) ?? false));
            }
            else
            {
                type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t =>
                        t.IsClass == true
                        && t.IsAbstract == false
                        && t.GetInterface(interfaceType.Name) != null);
            }

            ArgumentNullException.ThrowIfNull(type, $"Cannot find class implemented this interface {interfaceType.Name}");

            return type;
        }

    }
}
