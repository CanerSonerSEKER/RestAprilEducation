using RestAprilEducation.Domain;

namespace RestAprilEducation.Application.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        Task<List<Product>> GetAllWithPagedAsync(int pageNumber, int pageSize);

        Task<Product?> AnyAsync(string name);
    }
}
