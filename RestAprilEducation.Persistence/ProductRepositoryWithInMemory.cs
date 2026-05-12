using RestAprilEducation.Application.Products;
using RestAprilEducation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Persistence
{
    public class ProductRepositoryWithInMemory : IProductRepository
    {
        public Task<bool> AnyAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
