namespace FlowershopAPI.DTOs
{
    public class ProductInput
    {
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class ProductDTO
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
    }
}
