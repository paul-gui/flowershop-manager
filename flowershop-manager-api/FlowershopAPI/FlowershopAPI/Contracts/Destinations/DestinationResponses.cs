using System.ComponentModel.DataAnnotations;

namespace FlowershopAPI.Contracts.Destinations;

public class DestinationResponse
{
    public required Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}