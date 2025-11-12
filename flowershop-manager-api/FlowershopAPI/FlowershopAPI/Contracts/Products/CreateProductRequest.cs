namespace FlowershopAPI.Contracts.Products;

public class CreateProductRequest
{
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyList<ProductPriceForDestination> Prices { get; set; }
}

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyList<ProductPriceForDestination> Prices { get; set; }
}

public class ProductPriceForDestination
{
    public Guid DestinationId { get; set; }
    public decimal Value { get; set; }
}