using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Entities.Models
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreateDate { get; set; }
    }
}
