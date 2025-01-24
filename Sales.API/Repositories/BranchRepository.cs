using Sales.API.Context;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext context) : base(context) { }
    }
}
