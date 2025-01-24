using Sales.API.DTO.Sale.Request;
using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Services
{
    public class SalesService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<SaleItem> _saleItemRepository;

        public SalesService(IRepository<Sale> saleRepository, IRepository<SaleItem> saleItemRepository)
        {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
        }

        public async Task<Sale> CreateSaleAsync(CreateSaleRequest request)
        {
            if (!request.Items.Any())
                throw new ArgumentException("A sale must have at least one item.");

            var sale = new Sale
            {
                SaleDate = DateTime.Now.ToUniversalTime(),
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                IsCancelled = false
            };

            var newSale = await _saleRepository.Add(sale);

            foreach (var item in request.Items)
            {
                if (item.Quantity < 1)
                    throw new ArgumentException("Quantity must be at least 1.");
                if (item.Quantity > 20)
                    throw new ArgumentException($"Item {item.ProductId} cannot have more than 20 units.");

                var saleItem = new SaleItem
                {
                    SaleId = newSale.Id,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    UnityPrice = item.UnitPrice
                };

                if (saleItem.Quantity >= 4 && saleItem.Quantity < 10)
                    saleItem.UnityPrice = item.UnitPrice * 0.9m;
                else if (item.Quantity >= 10)
                    saleItem.UnityPrice = item.UnitPrice * 0.8m;
                

                await _saleItemRepository.Add(saleItem);
            }

            return sale;
        }

        public async Task<Sale> GetSaleByIdAsync(Guid saleId)
        {
            var sale = await _saleRepository.GetById(saleId);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");
            return sale;
        }

        public async Task CancelSaleAsync(Guid saleId)
        {
            var sale = await _saleRepository.GetById(saleId);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            if (sale.IsCancelled)
                throw new InvalidOperationException("Sale is already cancelled.");

            sale.IsCancelled = true;

            _saleRepository.Update(sale);
        }

        public async Task<List<Sale>> GetAllSalesAsync(int skip = 0, int take = 0)
        {
            return await _saleRepository.Get(skip, take);
        }

        public async Task DeleteSale(Guid saleId)
        {
            var sale = await _saleRepository.GetById(saleId);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            _saleRepository.Delete(sale);
        }
    }

}
