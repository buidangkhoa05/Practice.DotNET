using Domain.Common.Entities;
using Domain.Common.PagedList;
using Domain.Common.Queries.Interface;

namespace Domain.Common.Persistence
{
    public interface IGenericRepository<TEntity> : IRepositoryBase
        where TEntity : EntityBase
    {
        #region Query
        Task<TEntity?> FindAsync(int entityId);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>() where TResult : class;
        Task<bool> IsExistAsync(params int[] ids);
        #endregion Query

        #region Command 
        Task CreateAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task DeleteAsync(params int[] ids);
        #endregion Command 
    }
}
