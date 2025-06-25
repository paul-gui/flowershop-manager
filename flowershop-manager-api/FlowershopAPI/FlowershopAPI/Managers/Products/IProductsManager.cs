using FlowershopAPI.DTOs;

namespace FlowershopAPI.Managers.Products
{
    public interface IProductsManager
    {
        public Task<List<DTOs.ProductDTO>> GetProducts(Guid warehouseId);
        public Task<DTOs.ProductDTO> GetProduct(Guid Id);
        public Task<DTOs.ProductDTO> AddProduct(DTOs.ProductInput product);
        public Task<DTOs.ProductDTO> UpdateProduct(Guid Id, DTOs.ProductInput product);
        public Task<DTOs.ProductDTO> DeleteProduct(Guid id);
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
