namespace FlowershopAPI.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
        public List<PriceDTO> Prices { get; set; }
    }

    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
    }

    public class PriceDTO 
    {
        public Guid DestinationId { get; set; }
        public decimal Value { get; set; }
    }
    
    public class DestinationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
