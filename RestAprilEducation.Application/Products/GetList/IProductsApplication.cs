namespace RestAprilEducation.Application.Products.GetList
{
    public interface IProductsApplication
    {
        Task<List<ProductDto>> GetAll();
    }
}
 