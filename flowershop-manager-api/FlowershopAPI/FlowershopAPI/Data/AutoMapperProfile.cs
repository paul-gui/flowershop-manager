using AutoMapper;

namespace FlowershopAPI.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Models.Warehouse, DTOs.WarehouseDTO>();
            CreateMap<DTOs.WarehouseInput, Models.Warehouse>();

            CreateMap<Models.Product, DTOs.ProductDTO>();
            CreateMap<DTOs.ProductInput, Models.Product>();
        }
    }
}
