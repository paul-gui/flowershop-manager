namespace FlowershopAPI.Models;

public class Sale
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public Guid DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;
    
    public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;
    
    public DateTimeOffset CreatedAt { get; set; } =  DateTimeOffset.UtcNow;
    
    public int Quantity { get; set; }
    public decimal PriceAtSale { get; set; }

    public bool IsActive { get; set; } = true;
}