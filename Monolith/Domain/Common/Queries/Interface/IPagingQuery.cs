namespace Domain.Common.Queries.Interface
{
    public interface IPagingQuery<TResult> : IQuery<TResult>
        where TResult : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
