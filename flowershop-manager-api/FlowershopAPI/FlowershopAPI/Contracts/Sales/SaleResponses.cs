namespace FlowershopAPI.Contracts.Sales;

public class SaleResponse
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string WarehouseName { get; set; }
    public string DestinationName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtSale { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}