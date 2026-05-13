using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.GetList;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetByIdProductEndpoint
    {

        public static RouteGroupBuilder AddGetByIdProductEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id}", async ([FromRoute] int id, [FromServices]IProductsApplication productApplication) =>
            {
                var product = await productApplication.GetById(id);

                if (product == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(product);
            });

            return group;
        }


    }
}
