using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sales.API.Models
{
    public class SaleItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column]
        public Guid SaleId { get; set; }
        [Required]
        [Column]
        public Guid ProductId { get; set; }
        [Required]
        [Column]
        public int Quantity { get; set; }
        [Required]
        [Column]
        public decimal UnityPrice { get; set; }
        [Required]
        [Column]
        public decimal TotalPrice => (Quantity * UnityPrice);
        [JsonIgnore]
        public Sale Sale { get; set; }
        public Product Product {  get; set; }
    }
}
