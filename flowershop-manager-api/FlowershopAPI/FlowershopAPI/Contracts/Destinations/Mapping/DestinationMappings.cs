using AutoMapper;
using FlowerShopAPI.Models;

namespace FlowerShopAPI.Contracts.Destinations.Mapping;

public class DestinationMappings : Profile
{
    public DestinationMappings()
    {
        CreateMap<Destination, DestinationResponse>();
    }
}