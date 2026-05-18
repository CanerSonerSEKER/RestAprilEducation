using RestAprilEducation.Application.Products;
using RestAprilEducation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAprilEducation.Persistence
{
    public class ProductRepositoryWithInMemory : IProductRepository
    {
        private static readonly List<Product> _products;
        private static int _nextId;
        private static readonly object _lock = new object();

        static ProductRepositoryWithInMemory()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Sample Product A", Price = 10m, Barcode = "ABC123" },
                new Product { Id = 2, Name = "Sample Product B", Price = 20m, Barcode = "DEF456" }
            };
            _nextId = _products.Max(p => p.Id);
        }

        public Task<bool> AnyAsync(string productName)
        {
            if (productName == null) return Task.FromResult(false);
            var exists = _products.Any(p => string.Equals(p.Name, productName, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        public Task<Product> CreateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            lock (_lock)
            {
                product.Id = ++_nextId;
                // create a shallow copy to avoid external mutation
                var toAdd = new Product { Id = product.Id, Name = product.Name, Price = product.Price, Barcode = product.Barcode };
                _products.Add(toAdd);
                return Task.FromResult(toAdd);
            }
        }

        public Task<Product> DeleteAsync(int id)
        {
            lock (_lock)
            {
                var existing = _products.FirstOrDefault(p => p.Id == id);
                if (existing == null) return Task.FromResult<Product>(null);
                _products.Remove(existing);
                return Task.FromResult(existing);
            }
        }

        public Task<List<Product>> GetAll()
        {
            // return a copy to prevent callers mutating internal list
            var list = _products.Select(p => new Product { Id = p.Id, Name = p.Name, Price = p.Price, Barcode = p.Barcode }).ToList();
            return Task.FromResult(list);
        }

        public Task<List<Product>> GetAllWithPagedAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var skip = (pageNumber - 1) * pageSize;
            var paged = _products.Skip(skip).Take(pageSize).Select(p => new Product { Id = p.Id, Name = p.Name, Price = p.Price, Barcode = p.Barcode }).ToList();
            return Task.FromResult(paged);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult<Product>(null);
            var copy = new Product { Id = existing.Id, Name = existing.Name, Price = existing.Price, Barcode = existing.Barcode };
            return Task.FromResult(copy);
        }

        public Task<Product> UpdateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            lock (_lock)
            {
                var existing = _products.FirstOrDefault(p => p.Id == product.Id);
                if (existing == null) return Task.FromResult<Product>(null);
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Barcode = product.Barcode;
                var copy = new Product { Id = existing.Id, Name = existing.Name, Price = existing.Price, Barcode = existing.Barcode };
                return Task.FromResult(copy);
            }
        }
    }
}
