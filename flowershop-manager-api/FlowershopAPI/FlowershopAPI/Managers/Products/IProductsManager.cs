using FlowershopAPI.Contracts.Products;
using FlowershopAPI.DTOs;

namespace FlowershopAPI.Managers.Products
{
    public interface IProductsManager
    {
        public Task<List<DTOs.ProductDTO>> GetProducts(Guid warehouseId);
        public Task<DTOs.ProductDTO> GetProduct(Guid Id);
        public Task<DTOs.ProductDTO> AddProduct(CreateProductRequest createProduct);
        public Task<DTOs.ProductDTO> UpdateProduct(Guid Id, CreateProductRequest createProduct);
        public Task<DTOs.ProductDTO> DeleteProduct(Guid id);
        public Task<List<DTOs.DestinationDTO>> GetDestinations();
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
