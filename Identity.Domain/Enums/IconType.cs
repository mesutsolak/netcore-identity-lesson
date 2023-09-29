using System.ComponentModel;

namespace Identity.Domain.Enums
{
    public enum IconType
    {
        [Description("Seçilmedi")]
        None = 0,

        [Description("success")]
        Success = 1,

        [Description("error")]
        Error = 2
    }
}
