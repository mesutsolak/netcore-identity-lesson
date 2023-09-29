using Identity.Domain.Enums;
using Identity.Domain.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Domain.Models
{
    public sealed class JsonResponseModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
        public string Function { get; set; }
        public string Url { get; set; }
        public bool IsModal { get; set; }
        public string Title { get; set; }
        public bool IsSuccess => !Errors.Any();
        public string Icon => IsSuccess ? IconType.Success.GetEnumDescription() : IconType.Error.GetEnumDescription();
    }
}
