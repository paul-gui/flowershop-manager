using AutoMapper;
using FlowerShopAPI.Models;

namespace FlowerShopAPI.Contracts.Products.Mapping;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Price, PriceForProductResponse>()
            .ForMember(d => d.DestinationName,
                opt => opt.MapFrom(s => s.Destination.Name));
        CreateMap<PriceForProductRequest, Price>()
            .ForMember(d => d.Destination,  opt => opt.Ignore())
            .ForMember(d => d.Product, opt => opt.Ignore());
        CreateMap<Product, ProductResponse>();
        CreateMap<CreateProductRequest, Product>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Prices, opt => opt.Ignore());
        
    }
}