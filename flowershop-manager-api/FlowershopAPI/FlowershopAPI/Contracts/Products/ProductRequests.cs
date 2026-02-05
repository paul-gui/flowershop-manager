using System.ComponentModel.DataAnnotations;

namespace FlowerShopAPI.Contracts.Products;

public class CreateProductRequest
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required Guid WarehouseId { get; set; }
    
    [Required]
    public required IReadOnlyList<PriceForProductRequest> Prices { get; set; }
}

public class UpdateProductRequest
{
    [Required]
    public required Guid Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required Guid WarehouseId { get; set; }
    
    [Required]
    public required IReadOnlyList<PriceForProductRequest> Prices { get; set; }
}

public class PriceForProductRequest
{
    [Required]
    public Guid DestinationId { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Value { get; set; }
}