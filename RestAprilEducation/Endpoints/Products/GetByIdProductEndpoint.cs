using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application.Products;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetByIdProductEndpoint
    {

        public static RouteGroupBuilder AddGetByIdProductEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id}", 
                async ([FromRoute] int id, [FromServices]IProductsApplication productApplication) =>
                    (await productApplication.GetById(id)).ToResult());
            
            return group;
        }


    }
}
