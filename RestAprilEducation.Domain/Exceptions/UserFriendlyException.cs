using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain.Exceptions
{
    public class UserFriendlyException(string message) : Exception(message)
    {
        public int? StatusCode { get; set; }

    }
}
