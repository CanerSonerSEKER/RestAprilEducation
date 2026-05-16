using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Application.Products.Update;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class UpdateProductEndpoint
    {

        // PUT -- /api/products/1 request body : { "name": "Updated Product", "price": 19.99 }

        public static RouteGroupBuilder AddUpdateProductEndpoint(this RouteGroupBuilder group)
        {

            group.MapPut("/{id:int}", 
                async ([FromRoute]int id, [FromBody]UpdateProductRequest updateProductRequest, [FromServices]IProductsApplication productApplication) => 
                    (await productApplication.Update(id, updateProductRequest)).ToResult())
                .AddEndpointFilter<ValidatorFilter<UpdateProductRequest>>().MapToApiVersion(1, 0);

            return group;

        }

    }
}
