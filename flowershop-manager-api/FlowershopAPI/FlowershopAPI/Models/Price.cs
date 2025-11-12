namespace FlowershopAPI.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public Guid DestinationId { get; set; }
        public Destination Destination { get; set; }
        
        public decimal Value { get; set; }
    }
}
