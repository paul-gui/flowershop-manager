using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlowershopAPI.Common.Results;
using FlowershopAPI.Contracts.Sales;
using FlowershopAPI.Data;
using FlowershopAPI.Managers.Sales.Contract;
using FlowershopAPI.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<OperationResult<List<SaleResponse>>> GetSales(SalesFilterRequest filter)
    {
        var query = context.Sales.AsQueryable();

        if (filter.WarehouseId.HasValue)
        {
            query = query.Where(s => s.Product.WarehouseId == filter.WarehouseId.Value);
        }

        if (filter.DestinationId.HasValue)
        {
            query = query.Where(s => s.DestinationId == filter.DestinationId.Value);
        }

        if (filter.StartDate.HasValue)
        {
            DateTime.SpecifyKind(filter.StartDate.Value, DateTimeKind.Utc);
            query = query.Where(s => s.CreatedAt >= filter.StartDate.Value);

        }

        if (filter.EndDate.HasValue)
        {
            DateTime.SpecifyKind(filter.EndDate.Value, DateTimeKind.Utc);
            query = query.Where(s => s.CreatedAt <= filter.EndDate.Value);
        }
            
        
        var sales = await query.ToListAsync();
        
        //var result = mapper.Map<List<SaleResponse>>(sales);
        var result = await query
            .ProjectTo<SaleResponse>(mapper.ConfigurationProvider)
            .ToListAsync();

        return OperationResult<List<SaleResponse>>.Success(result);
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