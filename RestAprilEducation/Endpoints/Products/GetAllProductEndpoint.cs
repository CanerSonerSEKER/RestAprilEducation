using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.GetList;
using System.Reflection.Metadata.Ecma335;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetAllProductEndpoint
    {

        public static RouteGroupBuilder AddGetAllProductEndpoint(this RouteGroupBuilder group) 
        {
            group.MapGet("/", async ([FromServices]IProductsApplication productApplication) => 
            {
                var productList = await productApplication.GetAll();

                return Results.Ok(productList);
            });

            return group;
        }

    }
}
