using System.ComponentModel.DataAnnotations;

namespace FlowerShopAPI.Models
{
    public class Destination
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
