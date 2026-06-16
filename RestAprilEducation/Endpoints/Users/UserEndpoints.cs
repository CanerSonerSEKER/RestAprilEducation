using RestAprilEducation.API.Endpoints.Users.Create;

namespace RestAprilEducation.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserEndpoints(this WebApplication app)
        {
            app.MapGroup("api/users")
                .AddCreateUserEndpoint();
        }
    }
}
