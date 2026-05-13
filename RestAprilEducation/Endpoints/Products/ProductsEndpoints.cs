namespace RestAprilEducation.API.Endpoints.Products
{
    public static class ProductsEndpoints
    {

        // querystring = api/products?id=1
        // route data = api/products/1
        // body
        // header

        public static void AddProductEndpoints(this WebApplication app)
        {
            var productsGroup = app.MapGroup("/api/products")
                .AddCreateProductsEndpoints()
                .AddUpdateProductEndpoint()
                .AddDeleteProductEndpoint()
                .AddGetAllProductEndpoint()
                .AddGetByIdProductEndpoint()
                .AddGetAllWithPagedProductEndpoint();
        }

    }
}
