using System.ComponentModel.DataAnnotations;

namespace FlowershopAPI.Contracts.Sales;

public class SaleCreationRequest
{
    [Required]
    public required Guid ProductId { get; set; }
    
    [Required]
    public required Guid DestinationId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public required int Quantity { get; set; }
    
    [Required]
    public decimal PriceAtSale { get; set; }
}