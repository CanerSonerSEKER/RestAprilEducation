using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> CreateAsync(Product product);

        Task<bool> AnyAsync(string productName);

    }
}
