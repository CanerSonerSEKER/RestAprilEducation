using Microsoft.EntityFrameworkCore;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Persistence
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public Task<Product?> AnyAsync(string name)
        {
            return _dbSet.FirstOrDefaultAsync(p => p.Name == name);

        }

        public Task<List<Product>> GetAllWithPagedAsync(int pageNumber, int pageSize)
        {

            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
