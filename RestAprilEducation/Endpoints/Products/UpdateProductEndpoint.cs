using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.GetList;
using RestAprilEducation.Application.Products.Update;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class UpdateProductEndpoint
    {

        // PUT -- /api/products/1 request body : { "name": "Updated Product", "price": 19.99 }

        public static RouteGroupBuilder AddUpdateProductEndpoint(this RouteGroupBuilder group)
        {

            group.MapPut("/{id}", async ([FromRoute]int id, [FromBody]UpdateProductRequest updateProductRequest, [FromServices]IProductsApplication productApplication) => 
            {
                var product = await productApplication.Update(id, updateProductRequest);

                return Results.NoContent();

            });

            return group;

        }

    }
}
