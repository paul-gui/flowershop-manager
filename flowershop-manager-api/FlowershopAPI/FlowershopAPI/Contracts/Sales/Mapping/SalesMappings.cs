using AutoMapper;
using FlowershopAPI.Models;

namespace FlowershopAPI.Contracts.Sales.Mapping;

public class SalesMappings : Profile
{
    public SalesMappings()
    {
        CreateMap<SaleCreationRequest, Sale>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.User, opt => opt.Ignore())
            .ForMember(d => d.Destination, opt => opt.Ignore())
            .ForMember(d => d.Product, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore());
    }
}