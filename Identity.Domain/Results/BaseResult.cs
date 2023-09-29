using System.Collections.Generic;
using System.Linq;

namespace Identity.Domain.Results
{
    /// <summary>
    /// 
    /// 
    /// İlk overload çalıştığı için success true dönmektedir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BaseResult<T> : IBaseResult where T : class, new()
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public BaseResult(T data = null)
        {
            Data = data;
            IsSuccess = true;
        }

        public BaseResult(string message)
        {
            Messages.Add(message);
        }

        public BaseResult(IEnumerable<string> messages)
        {
            Messages = messages.ToList();
        }
    }
    public interface IBaseResult
    {

    }
}
