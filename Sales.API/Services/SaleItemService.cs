using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Services
{
    public class SaleItemService
    {
        private readonly IRepository<SaleItem> _saleItemRepository;

        public SaleItemService(IRepository<SaleItem> saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }

        public async Task<SaleItem> AddItemToSaleAsync(Guid saleId, Guid productId, int quantity, decimal unitPrice)
        {
            var item = new SaleItem
            {
                SaleId = saleId,
                ProductId = productId,
                Quantity = quantity,
            };

            await _saleItemRepository.Add(item);
            return item;
        }

        public async Task<IEnumerable<SaleItem>> GetAllItemsAsync(int skip = 0, int take = 0)
        {
            return await _saleItemRepository.Get(skip, take);
        }

        public async Task<SaleItem> GetItemByIdAsync(Guid id)
        {
            var item = await _saleItemRepository.GetById(id);
            if (item == null)
                throw new KeyNotFoundException("Sale item not found.");

            return item;
        }

        public async Task<SaleItem> UpdateItemAsync(Guid id, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1.");
            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 items of the same product.");

            var item = await _saleItemRepository.GetById(id);
            if (item == null)
                throw new KeyNotFoundException("Sale item not found.");

            item.Quantity = quantity;
            item.UnityPrice = unitPrice;

           _saleItemRepository.Update(item);
            return item;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _saleItemRepository.GetById(id);
            if (item == null)
                throw new KeyNotFoundException("Sale item not found.");

            _saleItemRepository.Delete(item);
        }
    }
}
