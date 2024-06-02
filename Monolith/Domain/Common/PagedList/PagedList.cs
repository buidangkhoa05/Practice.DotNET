using Domain.Common.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.PagedList
{
    public class PagedList<TEntity> : List<TEntity>, IPagedList<TEntity>
        where TEntity : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList()
        {
        }

        public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        /// <summary>
        /// Excute query with pagination
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task LoadData(IQueryable<TEntity> queryList, int pageNumber, int pageSize)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                throw new ArgumentException("Page number or page size must be greater than 0");
            }

            var items = await queryList
                                .AsNoTracking()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync()
                                .ConfigureAwait(false);

            TotalCount = queryList.Count();
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(queryList.Count() / (double)pageSize);

            this.AddRange(items);
        }

        public async Task LoadDataAsync(IQueryable<TEntity> queryList, IPagingQuery<TEntity> pagingQuery)
        {
            if (pagingQuery == null)
            {
                throw new ArgumentNullException(nameof(pagingQuery));
            }

            if (pagingQuery.PageSize <= 0 || pagingQuery.PageIndex <= 0)
            {
                throw new ArgumentException("Page number or page size must be greater than 0");
            }

            await LoadData(queryList, pagingQuery.PageIndex, pagingQuery.PageSize);
        }
    }
}
