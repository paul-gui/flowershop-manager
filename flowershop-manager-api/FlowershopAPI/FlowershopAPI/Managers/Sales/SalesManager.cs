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

        sale.SaleDate = saleCreationRequest.SaleDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        
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
            var startUtc = filter.StartDate.Value
                .ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            query = query.Where(s => s.SaleDate >= startUtc);
        }

        if (filter.EndDate.HasValue)
        {
            var endUtcExclusive = filter.EndDate.Value
                .AddDays(1)
                .ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            query = query.Where(s => s.SaleDate < endUtcExclusive);
        }
            
        
        var result = await query
            .Where(s => s.IsActive)
            .OrderBy(s => s.SaleDate)
            .ProjectTo<SaleResponse>(mapper.ConfigurationProvider)
            .ToListAsync();

        return OperationResult<List<SaleResponse>>.Success(result);
    }

    public async Task<OperationResult<SaleResponseForEdit>> GetSaleForEdit(Guid saleId)
    {
        var sale = await context.Sales.Where(s => s.IsActive).Include(s => s.Product).ThenInclude(s => s.Warehouse).FirstOrDefaultAsync(s => s.Id == saleId);

        if (sale == null)
        {
            return OperationResult<SaleResponseForEdit>.Failed(["Sale not found!"]);
        }
        var result = mapper.Map<SaleResponseForEdit>(sale);
        return OperationResult<SaleResponseForEdit>.Success(result);
    }

    public async Task<OperationResult<string>> UpdateSale(Guid saleId, SaleUpdateRequest saleUpdateRequest)
    {
        var sale = await context.Sales.FindAsync(saleId);
        var destination = await context.Destinations.FindAsync(saleUpdateRequest.DestinationId);
        
        if (sale is null || !sale.IsActive)
        {
            return OperationResult<string>.Failed(["Sale not found!"]);
        }
        
        if (destination == null)
        {
            return OperationResult<string>.Failed(["Destination not found!"]);
        }
        
        if (saleUpdateRequest.Quantity <= 0)
        {
            return OperationResult<string>.Failed(["Quantity must be greater than 0!"]);
        }
        
        if (saleUpdateRequest.PriceAtSale <= 0)
        {
            return OperationResult<string>.Failed(["Price must be greater than 0!"]);
        }
        
        sale.Quantity = saleUpdateRequest.Quantity;
        sale.PriceAtSale = saleUpdateRequest.PriceAtSale;
        sale.DestinationId = saleUpdateRequest.DestinationId;
        sale.SaleDate = saleUpdateRequest.SaleDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        
        await context.SaveChangesAsync();
        return OperationResult<string>.Success("Sale Updated");
    }

    public async Task<OperationResult<string>> DeleteSale(Guid saleId)
    {
        var sale = await context.Sales.FindAsync(saleId);
        if (sale == null)
        {
            return OperationResult<string>.Failed(["Sale not found!"]);
        }
        sale.IsActive = false;
        await context.SaveChangesAsync();
        return OperationResult<string>.Success("Sale Deleted");
    }
}