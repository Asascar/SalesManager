using Sales.API.Context;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context) { }
    }
}
