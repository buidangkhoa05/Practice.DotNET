using Domain.Common.PagedList;
using System.Net;

namespace Domain.Common
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? CurrentPage { get; set; } = null;
        public int? TotalPages { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public int? TotalCount { get; set; } = null;
        public bool? HasPrevious
        {
            get
            {
                if (CurrentPage == null)
                    return null;
                return CurrentPage > 1;
            }
        }
        public bool? HasNext
        {
            get
            {
                if (CurrentPage == null || TotalPages == null)
                    return null;

                return CurrentPage < TotalPages;
            }
        }
        public object? Data { get; set; } = null;

        public static ApiResponse Success<T>(T data)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ApiResponse Success<T>(IPagedList<T> data)
        {
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                Data = data,
                CurrentPage = data.CurrentPage,
                TotalPages = data.TotalPages,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount
            };
        }

        public static ApiResponse Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
