using Asp.Versioning.Builder;
using RestAprilEducation.API.Endpoints.Products.Create.V2;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class VersionExampleEndpoints
    {

        // querystring = api/products?id=1
        // route data = api/products/1
        // body
        // header

        public static void AddProductEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            // route constraint => /api/products/{id:int} => id'nin integer olduğunu belirtiriz. Böylece string bir id gelirse 404 döner.
            var productsGroup = app.MapGroup("/api/v{version:apiVersion}/products").WithApiVersionSet(apiVersionSet)
                .AddCreateProductsEndpoints()
                .AddCreateProductsV2Endpoints()
                .AddUpdateProductEndpoint()
                .AddDeleteProductEndpoint()
                .AddGetAllProductEndpoint()
                .AddGetByIdProductEndpoint()
                .AddGetAllWithPagedProductEndpoint();
        }

    }
}
