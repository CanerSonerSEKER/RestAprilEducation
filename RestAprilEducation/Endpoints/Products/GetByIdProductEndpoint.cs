using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application.Products;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetByIdProductEndpoint
    {

        public static RouteGroupBuilder AddGetByIdProductEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:int}", 
                async ([FromRoute] int id, [FromServices]IProductsApplication productApplication) =>
                    (await productApplication.GetById(id)).ToResult()).MapToApiVersion(1, 0);
            
            return group;
        }


    }
}
