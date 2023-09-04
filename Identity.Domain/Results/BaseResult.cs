namespace Identity.Domain.Results
{
    public sealed class BaseResult<T> : IBaseResult where T : class, new()
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseResult(T data = null)
        {
            Data = data;
            Success = true;
        }

        public BaseResult(string message)
        {
            Message = message;
        }
    }
    public interface IBaseResult
    {

    }
}
