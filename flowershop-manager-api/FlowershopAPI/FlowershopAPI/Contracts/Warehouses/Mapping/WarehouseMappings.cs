using AutoMapper;
using FlowerShopAPI.Contracts.Products;
using FlowerShopAPI.Models;

namespace FlowerShopAPI.Contracts.Warehouses.Mapping;

public class WarehouseMappings : Profile
{
    public WarehouseMappings()
    {
        CreateMap<Warehouse, WarehouseDetailsResponse>();
        CreateMap<Warehouse, WarehouseResponse>();
        CreateMap<CreateWarehouseRequest, Warehouse>();
    }
}