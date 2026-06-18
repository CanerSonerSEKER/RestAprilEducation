using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain
{
    public class AppUser  : IdentityUser<Guid>
    {
         
        public string? City { get; set; }
        
        public DateTime BirthDate { get; set; }

        public UserDetails UserDetails { get; set; }
    }
}
