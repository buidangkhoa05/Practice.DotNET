namespace Domain.Common.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        TRepostitoy Instance<TRepostitoy>(Type? entityType = null)
           where TRepostitoy : IRepositoryBase;
    }
}
