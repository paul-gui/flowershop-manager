namespace FlowershopAPI.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
