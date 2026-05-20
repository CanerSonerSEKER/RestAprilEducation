using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Domain.Exceptions
{
    public class BusinessException(string message) : Exception(message)
    {
        public string? ErrorDetail { get; set; }

    }
}
