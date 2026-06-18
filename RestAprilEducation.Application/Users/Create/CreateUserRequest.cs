using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Application.Users.Create
{
    public record CreateUserRequest(string Username, string Email, string Password, DateTime BirthDate);
}
