using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Services
{
    public class BranchService
    {
        private readonly IRepository<Branch> _branchRepository;

        public BranchService(IRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Branch> CreateBranchAsync(string name, string location)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Branch name is required.");

            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                Name = name,
                Address = location
            };

            await _branchRepository.Add(branch);
            return branch;
        }

        public async Task<List<Branch>> GetAllBranchesAsync(int skip = 0, int take = 0)
        {
            return await _branchRepository.Get(skip, take);
        }

        public async Task<Branch> GetBranchByIdAsync(Guid branchId)
        {
            var branch = await _branchRepository.GetById(branchId);
            if (branch == null)
                throw new KeyNotFoundException("Branch not found.");

            return branch;
        }
        public async Task<Branch> UpdateBranchAsync(Guid branchId, string name, string location)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Branch name is required.");

            var branch = await _branchRepository.GetById(branchId);
            if (branch == null)
                throw new KeyNotFoundException("Branch not found.");

            branch.Name = name;
            branch.Address = location;

            _branchRepository.Update(branch);
            return branch;
        }

        // Deletar uma filial
        public async Task DeleteBranchAsync(Guid branchId)
        {
            var branch = await _branchRepository.GetById(branchId);
            if (branch == null)
                throw new KeyNotFoundException("Branch not found.");

            _branchRepository.Delete(branch);
        }
    }
}
