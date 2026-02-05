using Microsoft.EntityFrameworkCore;

namespace FlowerShopAPI.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public Guid DestinationId { get; set; }
        public Destination Destination { get; set; } = null!;
        
        [Precision(18, 2)]
        public decimal Value { get; set; }
    }
}
