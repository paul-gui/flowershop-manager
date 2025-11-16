namespace FlowershopAPI.Contracts.Warehouses;

public class CreateWarehouseRequest
{
    public required string Name { get; set; }
}

public class UpdateWarehouseRequest
{
    public Guid Id { get; set; }
    public required  string Name { get; set; }
}