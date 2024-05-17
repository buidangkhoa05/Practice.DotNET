namespace Domain.Common
{
    public class Result
    {
        public object Data { get; set; } = null;
        public bool IsSuccess { get; set; }
        public string? Message { get; set; } = null;

        public static Result Success(object data)
        {
            return new Result
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
