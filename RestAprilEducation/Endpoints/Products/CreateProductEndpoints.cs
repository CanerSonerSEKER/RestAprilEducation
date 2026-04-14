using System.Diagnostics;

namespace RestAprilEducation.API.Endpoints.Products
{
    public static class CreateProductEndpoints
    {

        // api/products.. 
        public static RouteGroupBuilder AddCreateProductsEndpoints(this RouteGroupBuilder group)
        {
            //group.MapGet("/products", GetProducts);

            return group;
        }

    }
}
