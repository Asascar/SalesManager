using Sales.API.Context;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
