﻿using Sales.API.DTO.SaleItem.Request;

namespace Sales.API.DTO.Sale.Request
{
    public class CreateSaleRequest
    {
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public List<SaleItemRequest> Items { get; set; }
    }
}
