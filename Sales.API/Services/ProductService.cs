using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(string name, string description, decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price
            };

            await _productRepository.Add(product);
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync(int skip = 0, int take = 0)
        {
            return await _productRepository.Get(skip, take);
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return product;
        }

        public async Task<Product> UpdateProductAsync(Guid productId, string name, string description, decimal price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product name is required.");
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            var product = await _productRepository.GetById(productId);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            product.Name = name;
            product.Description = description;
            product.Price = price;

            _productRepository.Update(product);
            return product;
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            _productRepository.Delete(product);
        }
    }

}
