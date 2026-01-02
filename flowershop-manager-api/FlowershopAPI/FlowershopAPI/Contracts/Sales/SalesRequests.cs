using System.ComponentModel.DataAnnotations;

namespace FlowershopAPI.Contracts.Sales;

public class SaleCreationRequest
{
    [Required]
    public required Guid ProductId { get; set; }
    
    [Required]
    public required Guid DestinationId { get; set; }
    
    [Required]
    public required int Quantity { get; set; }
    
    [Required]
    public decimal PriceAtSale { get; set; }
}

public class SalesFilterRequest
{
    public Guid? WarehouseId { get; set; }
    public Guid? DestinationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}