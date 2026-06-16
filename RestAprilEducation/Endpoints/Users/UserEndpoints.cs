using RestAprilEducation.API.Endpoints.Users.Create;
using RestAprilEducation.API.Endpoints.Users.Login;

namespace RestAprilEducation.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            app.MapGroup("api/users")
                .AddCreateUserEndpoint()
                .AddLoginUserEndpoint();
        }
    }
}
