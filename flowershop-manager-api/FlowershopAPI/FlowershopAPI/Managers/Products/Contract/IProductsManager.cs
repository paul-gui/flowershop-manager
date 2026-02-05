using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Contracts.Destinations;
using FlowerShopAPI.Contracts.Products;

namespace FlowerShopAPI.Managers.Products.Contract
{
    public interface IProductsManager
    {
        public Task<OperationResult<string>> CreateProduct(CreateProductRequest createProduct);
        public Task<OperationResult<List<ProductResponse>>> GetProductsByWarehouseId(Guid warehouseId);
        public Task<OperationResult<ProductResponse>> GetProduct(Guid id);
        public Task<OperationResult<decimal>> GetPrice(Guid productId, Guid destinationId);
        public Task<OperationResult<string>> UpdateProduct(UpdateProductRequest createProduct);
        public Task<OperationResult<ProductResponse>> DeleteProduct(Guid id);
        public Task<OperationResult<List<DestinationResponse>>> GetDestinations();
    }
}
