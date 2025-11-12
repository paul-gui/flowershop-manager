namespace FlowershopAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
