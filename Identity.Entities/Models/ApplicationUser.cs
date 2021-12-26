using Microsoft.AspNetCore.Identity;

namespace Identity.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string HomeTown { get; set; }
        public bool Gender { get; set; }
    }
}
