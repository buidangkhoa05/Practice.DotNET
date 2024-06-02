using Domain.Common.Queries.Interface;

namespace Domain.Common.Queries.Implement
{
    public class PagingQuery<TResult> : IPagingQuery<TResult>
        where TResult : class
    {
        const int maxPageSize = 50;
        private int _pageSize = 0;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        public PagingQuery()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public PagingQuery(int? pageNumer, int? pageSize)
        {
            PageIndex = pageNumer ?? 1;
            PageSize = pageSize ?? 10;
        }
    }
}
