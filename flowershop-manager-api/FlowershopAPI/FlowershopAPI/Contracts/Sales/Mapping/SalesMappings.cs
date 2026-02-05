using AutoMapper;
using FlowerShopAPI.Models;

namespace FlowerShopAPI.Contracts.Sales.Mapping;

public class SalesMappings : Profile
{
    public SalesMappings()
    {
        CreateMap<SaleCreationRequest, Sale>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.User, opt => opt.Ignore())
            .ForMember(d => d.Destination, opt => opt.Ignore())
            .ForMember(d => d.Product, opt => opt.Ignore())
            .ForMember(d => d.SaleDate, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore());

        CreateMap<Sale, SaleResponse>()
            .ForMember(d => d.WarehouseName,
                opt => opt.MapFrom(s => s.Product.Warehouse.Name))
            .ForMember(d => d.ProductName,
                opt => opt.MapFrom(s => s.Product.Name))
            .ForMember(d => d.DestinationName,
                opt => opt.MapFrom(s => s.Destination.Name))
            .ForMember(d => d.SaleDate, 
                opt => opt.MapFrom(s => s.SaleDate));

        CreateMap<Sale, SaleResponseForEdit>()
            .ForMember(d => d.WarehouseName,
                opt => opt.MapFrom(s => s.Product.Warehouse.Name))
            .ForMember(d => d.ProductName,
                opt => opt.MapFrom(s => s.Product.Name))
            .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.User.Name));
    }
}