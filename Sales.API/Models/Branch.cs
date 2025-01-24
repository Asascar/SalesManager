using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Sales.API.Models
{
    public class Branch
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column]
        public string Name { get; set; }
        [Required]
        [Column]
        public string Address { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
