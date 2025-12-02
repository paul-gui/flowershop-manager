using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Sales;

namespace FlowershopAPI.Managers.Sales.Contract;

public interface ISalesManager
{
    public Task<OperationResult<string>> CreateSale();
    public Task<SaleResponse> GetSale();
    public Task<OperationResult<string>> UpdateSale();
    public Task<OperationResult<string>> DeleteSale();
}