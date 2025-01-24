using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sales.API.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column]
        public string Name { get; set; }
        [Required]
        [Column]
        public string Email { get; set; }
        [Required]
        [Column]
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Sale> Sales { get; set; }
    }
}
