using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Application.Products.Create;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class FilterExampleEndpoint
    {

        // api/products.. 
        public static RouteGroupBuilder AddFilterExampleEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", () => Results.Ok("filter endpoint"))
                .AddEndpointFilter(async (context, next) =>
            {
                // 1. Filter
                var response = await next(context);

                return response;
            }).AddEndpointFilter(async (context, next) =>
            {
                // 2. Filter
                var response = await next(context);

                return response;
            }).AddEndpointFilter(async (context, next) =>
            {
                // 3. Filter
                var response = await next(context);

                return response;
            });


            return group;
        }

    }
}
