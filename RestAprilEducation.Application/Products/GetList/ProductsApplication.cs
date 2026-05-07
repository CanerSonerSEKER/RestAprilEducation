using Microsoft.AspNetCore.Mvc;
using RestAprilEducation.Application.Products.Create;
using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products.GetList
{
    public class ProductsApplication(IProductRepository productRepository) : IProductsApplication 
    {
        public async Task<ApplicationResult<List<ProductDto>>> GetAll()
        {
            var productList = await productRepository.GetAll();

            var productAsDtoList = new List<ProductDto>();

            foreach (var product in productList) 
            {
                var productAsDto = new ProductDto(product.Id, product.Name, (product.Price + (product.Price * 0.18m)));

                productAsDtoList.Add(productAsDto);
            }

            return ApplicationResult<List<ProductDto>>.Success(productAsDtoList);
        }

        public async Task<ApplicationResult<CreateProductResponse>> Create(CreateProductRequest productRequest)
        {
            var hasProducts = await productRepository.AnyAsync(productRequest.Name);

            // Result Pattern => Instead of throwing an exception, we can return a result
            // object that contains information about the success or failure of the operation.
            // This allows us to handle errors more gracefully and avoid using exceptions for control flow.
            // Hem başarılı durumu hem de hata durumunu tek bir nesne üzerinden yönetebiliriz. Bu, kodun okunabilirliğini artırır ve hata yönetimini daha etkili hale getirir.

            if (hasProducts)
            {
                return ApplicationResult<CreateProductResponse>.Failure("Product already exists", 400);
            }

            var barcode = Guid.NewGuid().ToString();

            var product = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Barcode = barcode
            };

            var createdProduct = await productRepository.CreateAsync(product);

            return ApplicationResult<CreateProductResponse>.Success(new CreateProductResponse(createdProduct.Id));
        }

    }
}
 