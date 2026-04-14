using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

    }
}
