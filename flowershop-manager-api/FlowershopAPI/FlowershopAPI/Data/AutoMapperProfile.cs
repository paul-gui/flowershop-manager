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
            CreateMap<DTOs.CreateProductDTO, Models.Product>();
            
            CreateMap<Models.Price, DTOs.PriceDTO>();
            CreateMap<DTOs.PriceDTO, Models.Price>();
            
            CreateMap<Models.Destination, DTOs.DestinationDTO>();
            CreateMap<DTOs.DestinationDTO, Models.Destination>();
        }
    }
}
