using FlowershopAPI.Contracts.Destinations;
using FlowershopAPI.Contracts.Products;
namespace FlowershopAPI.Managers.Products
{
    public interface IProductsManager
    {
        public Task<ProductResponse> CreateProduct(CreateProductRequest createProduct);
        public Task<List<ProductResponse>> GetProducts(Guid warehouseId);
        public Task<ProductResponse?> GetProduct(Guid id);
        public Task<ProductResponse?> UpdateProduct(UpdateProductRequest createProduct);
        public Task<ProductResponse?> DeleteProduct(Guid id);
        public Task<List<DestinationResponse>> GetDestinations();
    }
}
