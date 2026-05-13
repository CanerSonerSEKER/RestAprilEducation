using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application.Products;
using System.Reflection.Metadata.Ecma335;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetAllProductEndpoint
    {

        public static RouteGroupBuilder AddGetAllProductEndpoint(this RouteGroupBuilder group) 
        {
            group.MapGet("/", 
                async ([FromServices]IProductsApplication productApplication) => 
                    (await productApplication.GetAll()).ToResult());

            return group;
        }
    }


    public static class GetAllWithPagedProductEndpoint
    {

        public static RouteGroupBuilder AddGetAllWithPagedProductEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{pageNumber}/{pageSize}",
                async ([FromRoute]int pageNumber, [FromRoute]int pageSize, [FromServices] IProductsApplication productApplication) =>
                    (await productApplication.GetAllWithPaged(pageNumber, pageSize)).ToResult());

            return group;
        } 
    }


}
