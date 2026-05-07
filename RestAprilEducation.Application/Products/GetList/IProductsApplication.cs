namespace RestAprilEducation.Application.Products.GetList
{
    public interface IProductsApplication
    {
        Task<ApplicationResult<List<ProductDto>>> GetAll();
    }
}
 