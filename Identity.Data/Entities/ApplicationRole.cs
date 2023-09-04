using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Data.Entities
{
    public sealed class ApplicationRole : IdentityRole
    {
        public DateTime CreateDate { get; set; }
    }
}
