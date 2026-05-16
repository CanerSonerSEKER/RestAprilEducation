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
                    (await productApplication.GetAll()).ToResult()).MapToApiVersion(1, 0);

            return group;
        }
    }
}
