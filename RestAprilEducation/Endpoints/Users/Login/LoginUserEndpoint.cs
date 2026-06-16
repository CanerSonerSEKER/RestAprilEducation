using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Users.Login;

namespace RestAprilEducation.API.Endpoints.Users.Login
{
    public static class LoginUserEndpoint
    {
        public static RouteGroupBuilder AddLoginUserEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/login",
                async ([FromBody] LoginRequest request,
                       [FromServices] UserApplication userApplication) =>
                    (await userApplication.LoginAsync(request)).ToResult());

            return group;
        }
    }
}
