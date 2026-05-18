using Asp.Versioning.Builder;

namespace RestAprilEducation.API.Endpoints.Versioning
{
    public static class VersionExampleEndpoints
    {
        public static void AddVersionExampleEndpoint(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            // route constraint => /api/products/{id:int} => id'nin integer olduğunu belirtiriz. Böylece string bir id gelirse 404 döner.
            var versionGroupEndpoint = app.MapGroup("/api/version").WithApiVersionSet(apiVersionSet); 


            versionGroupEndpoint.MapGet("/", () =>  Results.Ok("v1")).MapToApiVersion(1, 0);
            versionGroupEndpoint.MapGet("/", () =>  Results.Ok("v2")).MapToApiVersion(2, 0);
            versionGroupEndpoint.MapGet("/", () =>  Results.Ok("v2.1")).MapToApiVersion(2, 1);

        }

    }
}
