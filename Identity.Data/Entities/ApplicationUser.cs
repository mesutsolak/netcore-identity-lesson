using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string HomeTown { get; set; }
        public bool Gender { get; set; }
    }
}
