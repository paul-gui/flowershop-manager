namespace FlowershopAPI.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid DestinationId { get; set; }
        public decimal ProductPrice { get; set; }

        public Product Product { get; set; }
        public Destination Destination { get; set; }
    }
}
