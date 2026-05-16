using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application.Products;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class GetAllWithPagedProductEndpoint
    {

        public static RouteGroupBuilder AddGetAllWithPagedProductEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{pageNumber:int}/{pageSize:int}",
                async ([FromRoute]int pageNumber, [FromRoute]int pageSize, [FromServices] IProductsApplication productApplication) =>
                    (await productApplication.GetAllWithPaged(pageNumber, pageSize)).ToResult()).MapToApiVersion(1, 0);

            return group;
        } 
    }


}
