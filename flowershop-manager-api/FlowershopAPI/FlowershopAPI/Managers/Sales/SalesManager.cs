using AutoMapper;
using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Sales;
using FlowershopAPI.Data;
using FlowershopAPI.Managers.Sales.Contract;
using FlowershopAPI.Models;

namespace FlowershopAPI.Managers.Sales;

public class SalesManager(ApplicationDbContext context, IMapper mapper) : ISalesManager
{
    public async Task<OperationResult<string>> CreateSale(string userId, SaleCreationRequest saleCreationRequest)
    {
        var sale = mapper.Map<Sale>(saleCreationRequest);
        
        var product = await context.Products.FindAsync(saleCreationRequest.ProductId);
        if (product == null)
        {
            return OperationResult<string>.Failed(["Product not found!"]);
        }
        
        var destination = await context.Destinations.FindAsync(saleCreationRequest.DestinationId);
        if (destination == null)
        {
            return OperationResult<string>.Failed(["Destination not found!"]);
        }

        if (saleCreationRequest.Quantity <= 0)
        {
            return OperationResult<string>.Failed(["Quantity must be greater than 0!"]);
        }

        if (saleCreationRequest.PriceAtSale <= 0)
        {
            return OperationResult<string>.Failed(["Price must be greater than 0!"]);
        }
        
        sale.UserId = userId;
        sale.CreatedAt = DateTimeOffset.UtcNow;
        
        context.Sales.Add(sale);
        await context.SaveChangesAsync();
        return OperationResult<string>.Success("Sale Created");
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