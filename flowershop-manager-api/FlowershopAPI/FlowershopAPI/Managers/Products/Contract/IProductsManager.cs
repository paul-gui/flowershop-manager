using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Destinations;
using FlowershopAPI.Contracts.Products;
namespace FlowershopAPI.Managers.Products
{
    public interface IProductsManager
    {
        public Task<ProductResponse> CreateProduct(CreateProductRequest createProduct);
        public Task<OperationResult<List<ProductResponse>>> GetProductsByWarehouseId(Guid warehouseId);
        public Task<OperationResult<ProductResponse>> GetProduct(Guid id);
        public Task<OperationResult<decimal>> GetPrice(Guid productId, Guid destinationId);
        public Task<OperationResult<string>> UpdateProduct(UpdateProductRequest createProduct);
        public Task<OperationResult<ProductResponse>> DeleteProduct(Guid id);
        public Task<List<DestinationResponse>> GetDestinations();
    }
}
