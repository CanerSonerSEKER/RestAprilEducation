using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.Create;
using RestAprilEducation.Application.Products.GetList;
using System.Diagnostics;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class CreateProductEndpoints
    {

        // api/products.. 
        public static RouteGroupBuilder AddCreateProductsEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", async  ([FromBody]CreateProductRequest productRequest, [FromServices]IProductsApplication productApplication) => 
            {
                var products = await productApplication.Create(productRequest);



                return Results.Ok(products);
                
            });

            return group;
        }

    }
}
