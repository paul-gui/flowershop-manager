namespace FlowershopAPI.Contracts.Products;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyList<PriceForProductResponse> Prices { get; set; }
}

public class PriceForProductResponse
{
    public Guid DestinationId { get; set; }
    public string DestinationName { get; set; }
    public decimal Value { get; set; }
}