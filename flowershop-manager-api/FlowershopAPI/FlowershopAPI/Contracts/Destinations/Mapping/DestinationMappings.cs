using AutoMapper;
using FlowershopAPI.Models;

namespace FlowershopAPI.Contracts.Destinations.Mapping;

public class DestinationMappings : Profile
{
    public DestinationMappings()
    {
        CreateMap<Destination, DestinationResponse>();
    }
}