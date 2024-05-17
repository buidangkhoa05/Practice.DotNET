using Domain.Common.Entity;
using Domain.Common.PagedList;

namespace Domain.Persistence.Common
{
    public interface IGenericRepository<TEntity> : IRepositoryBase 
        where TEntity : EntityBase
    {
        #region Query
        Task<TEntity?> FindAsync(int entityId);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>() where TResult : class;
        Task<IPagedList<TEntity>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
        Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
            where TResult : class;
        Task<bool> IsExistAsync(params int[] ids);
        #endregion Query

        #region Command 
        Task CreateAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task DeleteAsync(params int[] ids);
        #endregion Command 
    }
}
