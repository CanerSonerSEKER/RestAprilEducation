using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.GetList;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class DeleteProductEndpoint
    {

        public static RouteGroupBuilder AddDeleteProductEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/{id}", async ([FromRoute] int id, [FromServices] IProductsApplication productApplication) =>
            {

                var result = await productApplication.Delete(id);

                return Results.Ok();

            });

            return group;

        }


        

    }
}
