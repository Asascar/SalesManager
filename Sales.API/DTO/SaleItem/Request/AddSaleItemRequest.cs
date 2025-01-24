namespace Sales.API.DTO.SaleItem.Request
{
    public class AddSaleItemRequest
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
