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
        public async Task<ProductResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            var warehouse = await context.Warehouses.FindAsync(createProductRequest.WarehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }
            
            var product = mapper.Map<Models.Product>(createProductRequest);
            product.Prices = new List<Price>();

            foreach (var priceInput in createProductRequest.Prices)
            {
                var destination = await context.Destinations.FindAsync(priceInput.DestinationId);
                if (destination == null)
                {
                    throw new Exception("Destination not found");
                }
                product.Prices.Add(new Price
                {
                    DestinationId = destination.Id,
                    Value = priceInput.Value
                });
            }

            context.Add(product);
            await context.SaveChangesAsync();

            var result = mapper.Map<ProductResponse>(product);

            return result;
        }
        
        public async Task<OperationResult<List<ProductResponse>>> GetProductsByWarehouseId(Guid warehouseId)
        {
            var warehouse = await context.Warehouses.FindAsync(warehouseId);
            if (warehouse == null)
            {
                return OperationResult<List<ProductResponse>>.Failed(["Warehouse not found"]);
            }
            
            var products = await context.Products.Where(p => p.WarehouseId == warehouseId).ToListAsync();
            var result = mapper.Map<List<ProductResponse>>(products);

            return OperationResult<List<ProductResponse>>.Success(result);
        }

        public async Task<ProductResponse?> GetProduct(Guid id)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<ProductResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            
            if (product == null)
                return null;
            
            return product;
        }

        public async Task<OperationResult<decimal>> GetPrice(Guid productId, Guid destinationId)
        {
            var price = await context.Prices.FirstOrDefaultAsync(p => p.ProductId == productId && p.DestinationId == destinationId);
            
            if (price == null)
                return OperationResult<decimal>.Failed(["Price not found"]);
            
            return OperationResult<decimal>.Success(price.Value);
        }

        public async Task<ProductResponse?> UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = await context.Products.Include(p => p.Prices).FirstOrDefaultAsync(p => p.Id == updateProductRequest.Id);
            if (product == null)
                return null;
            
            product.Name = updateProductRequest.Name;
            product.Prices = mapper.Map<List<Price>>(updateProductRequest.Prices);
            
            await context.SaveChangesAsync();
            
            var result = mapper.Map<ProductResponse>(product);
            
            return result;
        }

        public async Task<ProductResponse?> DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return null;


            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return mapper.Map<ProductResponse>(product);

        }

        public async Task<List<DestinationResponse>> GetDestinations()
        {
            var destinations = await context.Destinations.ToListAsync();
            var result = mapper.Map<List<DestinationResponse>>(destinations);
            return result;
        }
    }
}
