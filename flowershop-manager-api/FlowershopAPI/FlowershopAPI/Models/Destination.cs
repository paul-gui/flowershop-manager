namespace FlowershopAPI.Models
{
    public class Destination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
