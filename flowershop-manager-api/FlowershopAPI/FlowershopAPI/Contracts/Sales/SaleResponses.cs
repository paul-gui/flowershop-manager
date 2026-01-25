namespace FlowershopAPI.Contracts.Sales;

public class SaleResponse
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PriceAtSale { get; set; }
    public DateTimeOffset SaleDate { get; set; }
}