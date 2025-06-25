namespace FlowershopAPI.Models
{
    public class Destination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        List<Price> Prices { get; set; }
    }
}
