namespace FlowershopAPI.DTOs
{
    public class WarehouseInput
    {
        public required string Name { get; set; }
    }

    public class WarehouseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<DTOs.ProductDTO> Products { get; set; }
    }
}
