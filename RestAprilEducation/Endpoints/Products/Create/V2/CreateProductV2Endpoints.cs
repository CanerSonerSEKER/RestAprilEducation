using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Application.Products.Create;

namespace RestAprilEducation.API.Endpoints.Products.Create.V2
{
    public static class CreateProductV2Endpoints
    {

        // api/products.. 
        public static RouteGroupBuilder AddCreateProductsV2Endpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", 
                async  ([FromServices]IProductsApplication productApplication) => Results.Ok("AddCreateProductsV2Endpoints"))
                .AddEndpointFilter<ValidatorFilter<CreateProductRequest>>().MapToApiVersion(2,0);

            return group;
        }

    }
}
