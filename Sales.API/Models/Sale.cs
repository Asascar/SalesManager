using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sales.API.DTO.Sale.Request;

namespace Sales.API.Models
{
    public class Sale
    {
        [Key]
        public Guid Id { get; set; }
        [Column]
        [Required]  
        public DateTime SaleDate { get; set; }
        [Column]
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Column]
        [Required]
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<SaleItem> Items { get; private set; }
        [Column]
        [Required]
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

        [Column]
        [Required]
        public bool IsCancelled { get; set; }


    }
}
