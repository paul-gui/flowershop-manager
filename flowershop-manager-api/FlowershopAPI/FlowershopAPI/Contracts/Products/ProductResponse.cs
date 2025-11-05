namespace FlowershopAPI.Contracts.Products;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WarehouseID { get; set; }
    public IReadOnlyList<PriceForProductResponse> Prices { get; set; }
}

public class PriceForProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}