using System.ComponentModel.DataAnnotations;

namespace FlowerShopAPI.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public bool IsActive { get; set; } = true;
    }
}
