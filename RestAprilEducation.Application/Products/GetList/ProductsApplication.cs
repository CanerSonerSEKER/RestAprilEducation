using RestAprilEducation.Application.Products.Create;
using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products.GetList
{
    public class ProductsApplication(IProductRepository productRepository) : IProductsApplication 
    {
        public async Task<List<ProductDto>> GetAll()
        {
            var productList = await productRepository.GetAll();

            var productAsDtoList = new List<ProductDto>();

            foreach (var product in productList) 
            {
                var productAsDto = new ProductDto(product.Id, product.Name, (product.Price + (product.Price * 0.18m)));

                productAsDtoList.Add(productAsDto);
            }

            return productAsDtoList;
        }

        public async Task<CreateProductResponse> Create(CreateProductRequest productRequest)
        {
            var barcode = Guid.NewGuid().ToString();

            var product = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Barcode = barcode
            };

            var createdProduct = await productRepository.CreateAsync(product);

            return new CreateProductResponse(createdProduct.Id);
        }

    }
}
 