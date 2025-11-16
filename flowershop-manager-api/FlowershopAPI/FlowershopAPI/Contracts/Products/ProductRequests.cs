namespace FlowershopAPI.Contracts.Products;

public class CreateProductRequest
{
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyList<PriceForProductRequest> Prices { get; set; }
}

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyList<PriceForProductRequest> Prices { get; set; }
}

public class PriceForProductRequest
{
    public Guid DestinationId { get; set; }
    public decimal Value { get; set; }
}