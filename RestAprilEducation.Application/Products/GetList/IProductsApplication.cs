using RestAprilEducation.Application.Products.Create;
using RestAprilEducation.Application.Products.Update;

namespace RestAprilEducation.Application.Products.GetList
{
    public interface IProductsApplication
    {
        Task<ApplicationResult<List<ProductDto>>> GetAll();
        Task<ApplicationResult<ProductDto>> GetById(int id);
        Task<ApplicationResult<CreateProductResponse>> Create(CreateProductRequest createRequest);
        Task<ApplicationResult<UpdateProductResponse>> Update(int id, UpdateProductRequest updateRequest);
        Task<ApplicationResult> Delete(int id);
    }
}
 