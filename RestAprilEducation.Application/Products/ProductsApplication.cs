using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestAprilEducation.Application.Products.Create;
using RestAprilEducation.Application.Products.GetList;
using RestAprilEducation.Application.Products.Update;
using RestAprilEducation.Domain;
using System.Net;
using System.Security.Claims;

namespace RestAprilEducation.Application.Products
{
    public class ProductsApplication(
        IProductRepository productRepository, 
        ILogger<ProductsApplication> logger, 
        ILoggerFactory loggerFactory,
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor) : IProductsApplication
    {
        public async Task<ApplicationResult<List<ProductDto>>> GetAll()
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);


            logger.LogInformation("Get all methodu çalıştı.");

            var loggerFromFactory = loggerFactory.CreateLogger("ProductsApplicationLogger");
            loggerFromFactory.LogInformation("Get all methodu çalıştı.");


            var productList = await productRepository.GetAllAsync();

            var productAsDtoList = new List<ProductDto>();

            foreach (var product in productList) 
            {
                var productAsDto = new ProductDto(product.Id, product.Name, (product.Price + (product.Price * 0.18m)));

                productAsDtoList.Add(productAsDto);
            }

            return ApplicationResult<List<ProductDto>>.Success(productAsDtoList, HttpStatusCode.OK);
        }

        public async Task<ApplicationResult<List<ProductDto>>> GetAllWithPaged(int pageNumber, int pageSize)
        {
            var productList = await productRepository.GetAllWithPagedAsync(pageNumber, pageSize);

            var productAsDtoList = new List<ProductDto>();

            foreach (var product in productList)
            {
                var productAsDto = new ProductDto(product.Id, product.Name, (product.Price + (product.Price * 0.18m)));

                productAsDtoList.Add(productAsDto);
            }

            return ApplicationResult<List<ProductDto>>.Success(productAsDtoList, HttpStatusCode.OK);
        }


        public async Task<ApplicationResult<ProductDto>> GetById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ApplicationResult<ProductDto>.Failure("Product not found", HttpStatusCode.NotFound);
            }

            var productAsDto = new ProductDto(product.Id, product.Name, product.Price);

            return ApplicationResult<ProductDto>.Success(productAsDto, HttpStatusCode.OK);
        }


        public async Task<ApplicationResult<CreateProductResponse>> Create(CreateProductRequest productRequest)
        {
            var hasProducts = await productRepository.AnyAsync(productRequest.Name);

            // Result Pattern => Instead of throwing an exception, we can return a result
            // object that contains information about the success or failure of the operation.
            // This allows us to handle errors more gracefully and avoid using exceptions for control flow.
            // Hem başarılı durumu hem de hata durumunu tek bir nesne üzerinden yönetebiliriz. Bu, kodun okunabilirliğini artırır ve hata yönetimini daha etkili hale getirir.

            if (hasProducts is not null)
            {
                return ApplicationResult<CreateProductResponse>.Failure("Product already exists", HttpStatusCode.BadRequest);
            }

            var barcode = Guid.NewGuid().ToString();

            var product = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Barcode = barcode,
                CategoryId = productRequest.CategoryId
            };

            await productRepository.AddAsync(product);
            // Veri tabanına yansıtma işlemini yapıyoruz.
            await unitOfWork.CommitAsync();

            return ApplicationResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id), HttpStatusCode.Created);
        }

        public async Task<ApplicationResult<UpdateProductResponse>> Update(int id, UpdateProductRequest updateRequest)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ApplicationResult<UpdateProductResponse>.Failure("Product not found", HttpStatusCode.NotFound);
            }

            var hasProductWithSameName = await productRepository.AnyAsync(updateRequest.Name);

            if (hasProductWithSameName != null && product.Name != updateRequest.Name)
            {
                return ApplicationResult<UpdateProductResponse>.Failure("Product with the same name already exists", HttpStatusCode.BadRequest);
            }

            product.Name = updateRequest.Name;
            product.Price = updateRequest.Price;

            await productRepository.UpdateAsync(product);
            await unitOfWork.CommitAsync();

            return ApplicationResult<UpdateProductResponse>.Success(new UpdateProductResponse(product.Id), HttpStatusCode.OK);

        }

        public async Task<ApplicationResult> Delete(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ApplicationResult.Failure("Product not found", HttpStatusCode.NotFound);
            }

            await productRepository.DeleteAsync(product);

            await unitOfWork.CommitAsync();

            return ApplicationResult.Success();
        }
    }
}
 