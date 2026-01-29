using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlowershopAPI.Common.Results;
using FlowershopAPI.Data;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using FlowershopAPI.Contracts;
using FlowershopAPI.Contracts.Destinations;
using FlowershopAPI.Contracts.Products;

namespace FlowershopAPI.Managers.Products
{
    public class ProductsManager(ApplicationDbContext context, IMapper mapper) : IProductsManager
    {
        public async Task<OperationResult<string>> CreateProduct(CreateProductRequest createProductRequest)
        {
            var warehouse = await context.Warehouses.FindAsync(createProductRequest.WarehouseId);
            if (warehouse is null || !warehouse.IsActive)
            {
                return OperationResult<string>.Failed(["Warehouse not found"]);
            }
            
            var product = mapper.Map<Product>(createProductRequest);
            product.Prices = new List<Price>();

            foreach (var priceInput in createProductRequest.Prices)
            {
                var destination = await context.Destinations.FindAsync(priceInput.DestinationId);
                if (destination == null)
                {
                    return OperationResult<string>.Failed(["Destination not found"]);
                }
                product.Prices.Add(new Price
                {
                    DestinationId = destination.Id,
                    Value = priceInput.Value
                });
            }

            context.Add(product);
            await context.SaveChangesAsync();

            return OperationResult<string>.Success("Product created");
        }

        public async Task<OperationResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Where(p => p.Id == id)
                .ProjectTo<ProductResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return OperationResult<ProductResponse>.Failed(["Product not found"]);
            }
            
            var result = mapper.Map<ProductResponse>(product);
            return OperationResult<ProductResponse>.Success(result);
        }
        
        public async Task<OperationResult<List<ProductResponse>>> GetProductsByWarehouseId(Guid warehouseId)
        {
            var warehouse = await context.Warehouses.FindAsync(warehouseId);
            if (warehouse is null || !warehouse.IsActive)
            {
                return OperationResult<List<ProductResponse>>.Failed(["Warehouse not found"]);
            }
            
            var products = await context.Products.Where(p => p.IsActive).Where(p => p.WarehouseId == warehouseId).ToListAsync();
            var result = mapper.Map<List<ProductResponse>>(products);

            return OperationResult<List<ProductResponse>>.Success(result);
        }

        public async Task<OperationResult<decimal>> GetPrice(Guid productId, Guid destinationId)
        {
            var price = await context.Prices.FirstOrDefaultAsync(p => p.ProductId == productId && p.DestinationId == destinationId);
            
            if (price == null)
                return OperationResult<decimal>.Failed(["Price not found"]);
            
            return OperationResult<decimal>.Success(price.Value);
        }

        public async Task<OperationResult<string>> UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = await context.Products.Where(p => p.IsActive).Include(p => p.Prices).FirstOrDefaultAsync(p => p.Id == updateProductRequest.Id);
            if (product == null)
            {
                return OperationResult<string>.Failed(["Product not found"]);
            }
            
            product.Name = updateProductRequest.Name;
            product.Prices = mapper.Map<List<Price>>(updateProductRequest.Prices);
            
            await context.SaveChangesAsync();
            
            return OperationResult<string>.Success("Product updated");
        }

        public async Task<OperationResult<ProductResponse>> DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return OperationResult<ProductResponse>.Failed(["Product not found"]);
            }
            
            product.IsActive = false;
            await context.SaveChangesAsync();

            return OperationResult<ProductResponse>.Success(mapper.Map<ProductResponse>(product));
        }

        public async Task<OperationResult<List<DestinationResponse>>> GetDestinations()
        {
            var destinations = await context.Destinations.ToListAsync();
            if (destinations.Count == 0)
            {
                return OperationResult<List<DestinationResponse>>.Failed(["No destinations found"]);
            }
            var result = mapper.Map<List<DestinationResponse>>(destinations);
            return OperationResult<List<DestinationResponse>>.Success(result);
        }
    }
}
