namespace Domain.Common.PagedList
{
    public class PagingResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public IPagedList<T> Data { get; set; } = default;
    }
}