using FlowerShopAPI.Contracts.Products;

namespace FlowerShopAPI.Contracts.Warehouses;

public class WarehouseDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductResponse> Products { get; set; } = [];
}

public class WarehouseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}