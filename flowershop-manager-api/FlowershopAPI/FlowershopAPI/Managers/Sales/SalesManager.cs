using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Sales;
using FlowershopAPI.Managers.Sales.Contract;

namespace FlowershopAPI.Managers.Sales;

public class SalesManager : ISalesManager
{
    public Task<OperationResult<string>> CreateSale()
    {
        throw new NotImplementedException();
    }

    public Task<SaleResponse> GetSale()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<string>> UpdateSale()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<string>> DeleteSale()
    {
        throw new NotImplementedException();
    }
}