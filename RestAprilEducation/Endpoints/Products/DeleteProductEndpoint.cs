using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class DeleteProductEndpoint
    {

        public static RouteGroupBuilder AddDeleteProductEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/{id:int}", 
                async ([FromRoute] int id, [FromServices] IProductsApplication productApplication) =>
                    (await productApplication.Delete(id)).ToResult()).MapToApiVersion(1,0);

            return group;

        }


        

    }
}
