using AutoMapper;
using FlowershopAPI.Contracts.Products;
using FlowershopAPI.Models;

namespace FlowershopAPI.Contracts.Warehouses.Mapping;

public class WarehouseMappings : Profile
{
    public WarehouseMappings()
    {
        CreateMap<Warehouse, WarehouseDetailsResponse>();
        CreateMap<Warehouse, WarehouseResponse>();
        CreateMap<CreateWarehouseRequest, Warehouse>();
    }
}