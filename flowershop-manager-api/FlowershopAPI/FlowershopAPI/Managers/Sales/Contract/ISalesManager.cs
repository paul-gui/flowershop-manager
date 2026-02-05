using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Contracts.Sales;

namespace FlowerShopAPI.Managers.Sales.Contract;

public interface ISalesManager
{
    public Task<OperationResult<string>> CreateSale(string userId, SaleCreationRequest saleCreationRequest);
    public Task<OperationResult<List<SaleResponse>>> GetSales(SalesFilterRequest salesFilterRequest);
    public Task<OperationResult<SaleResponseForEdit>> GetSaleForEdit(Guid saleId);
    public Task<OperationResult<string>> UpdateSale(Guid saleId, SaleUpdateRequest saleUpdateRequest);
    public Task<OperationResult<string>> DeleteSale(Guid saleId);
}