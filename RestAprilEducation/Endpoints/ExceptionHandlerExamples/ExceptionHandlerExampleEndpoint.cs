
using Asp.Versioning.Builder;
using RestAprilEducation.Domain.Exceptions;

namespace RestAprilEducation.API.Endpoints
{
    public static class ExceptionHandlerExampleEndpoint
    {

        // api/products.. 
        public static void AddExceptionHandlerExampleEndpoint(this WebApplication app)
        {

            app.MapPost("/api/exception-handler-example", () =>
            {
                // throw new Exception("Veri tabanı bağlantı hatası.");

                // throw new UserFriendlyException("UserFriendlyException error");

               // throw new BusinessException("business logic error");

                return Results.Ok();
            });

        }

    }
}
