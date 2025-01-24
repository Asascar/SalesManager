using Sales.API.Context;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class SaleItemRepository : Repository<SaleItem>, ISaleItemRepository

    {
        public SaleItemRepository(AppDbContext context) : base(context) { }
    }
}
