using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Users.Create;

namespace RestAprilEducation.API.Endpoints.Users.Create
{
    public static class CreateUserEndpoint
    {
        public static RouteGroupBuilder AddCreateUserEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                async ([FromBody] CreateUserRequest request, 
                        [FromServices] UserApplication userApplication) =>
                    (await userApplication.CreateUserAsync(request)).ToResult());

            return group;
        }

    }
}
