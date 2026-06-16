using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain
{
    public class UserDetails
    {
        public Guid UserId { get; set; }

        public string Address { get; set; }

        public AppUser AppUser { get; set; }

    }
}
