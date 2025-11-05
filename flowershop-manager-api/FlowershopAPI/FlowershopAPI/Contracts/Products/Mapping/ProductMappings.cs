using AutoMapper;
using FlowershopAPI.Models;

namespace FlowershopAPI.Contracts.Products.Mapping;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.Prices, opt => opt.Ignore());
        CreateMap<CreateProductRequest, Product>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Prices, opt => opt.Ignore());
    }
}