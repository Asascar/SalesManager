using Sales.API.Context;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context) { }
    }
}
