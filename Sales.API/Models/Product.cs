using System.ComponentModel.DataAnnotations;

namespace Sales.API.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }
}
