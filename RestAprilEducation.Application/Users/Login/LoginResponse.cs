using System;

namespace RestAprilEducation.Application.Users.Login
{
    public record LoginResponse(string Token, DateTime Expiration);
}
