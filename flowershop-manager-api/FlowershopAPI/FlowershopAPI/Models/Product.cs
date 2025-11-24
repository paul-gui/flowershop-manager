using System.ComponentModel.DataAnnotations;

namespace FlowershopAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        
        public ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
