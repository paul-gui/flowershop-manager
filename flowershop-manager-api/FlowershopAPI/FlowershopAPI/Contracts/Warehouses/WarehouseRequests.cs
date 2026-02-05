using System.ComponentModel.DataAnnotations;

namespace FlowerShopAPI.Contracts.Warehouses;

public class CreateWarehouseRequest
{
    [Required]
    public required string Name { get; set; }
}

public class UpdateWarehouseRequest
{
    [Required]
    public required Guid Id { get; set; }
    
    [Required]
    public required  string Name { get; set; }
}