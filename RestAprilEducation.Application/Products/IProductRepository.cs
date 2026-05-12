using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetByIdAsync(int id);

        Task<Product> CreateAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task<Product> DeleteAsync(int id);

        Task<bool> AnyAsync(string productName);

    }
}
