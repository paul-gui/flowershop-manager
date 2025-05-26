namespace FlowershopAPI.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public class WarehouseNotFoundException : Exception
    {
        public WarehouseNotFoundException() { }
        public WarehouseNotFoundException(string message) : base(message) { }
        public WarehouseNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
