using Identity.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string Picture { get; set; }
        public string HomeTown { get; set; }
        public Gender Gender { get; set; }
    }
}
