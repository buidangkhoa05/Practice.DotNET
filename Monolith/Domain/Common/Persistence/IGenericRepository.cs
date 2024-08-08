using Domain.Common.Entities;
using Domain.Common.PagedList;
using Domain.Common.Queries.Interface;

namespace Domain.Common.Persistence
{
    public interface IGenericRepository<TEntity> : IRepositoryBase
        where TEntity : EntityBase<string>
    {
       
    }
}
