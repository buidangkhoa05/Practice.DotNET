using Domain.Common.PagedList;

namespace Domain.Common.Queries.Interface
{
    public interface IPagingQueryHandler<TQuery, TResult>
        where TQuery : IPagingQuery<TResult>
        where TResult : class
    {
        Task<IPagedList<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
