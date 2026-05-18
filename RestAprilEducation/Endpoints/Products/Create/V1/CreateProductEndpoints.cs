using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Application.Products.Create;

namespace RestAprilEducation.API.Endpoints.Products.Create.V2
{
    public static class CreateProductEndpoints
    {

        // api/products.. 
        public static RouteGroupBuilder AddCreateProductsEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", 
                async  ([FromBody]CreateProductRequest productRequest, [FromServices]IProductsApplication productApplication) =>
                    (await productApplication.Create(productRequest)).ToResult())
                .AddEndpointFilter<ValidatorFilter<CreateProductRequest>>().MapToApiVersion(1,0);

            return group;
        }

    }
}
 