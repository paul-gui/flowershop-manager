using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlowershopAPI.Data;
using FlowershopAPI.DTOs;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using FlowershopAPI.Contracts;
using FlowershopAPI.Contracts.Products;

namespace FlowershopAPI.Managers.Products
{
    public class ProductsManager : IProductsManager
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DTOs.ProductDTO>> GetProducts(Guid warehouseId)
        {
            var products = await _context.Products.Where(p => p.WarehouseId == warehouseId).ToListAsync();
            var result = _mapper.Map<List<DTOs.ProductDTO>>(products);

            return result;
        }

        public async Task<ProductResponse?> GetProduct(Guid id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<ProductResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            
            if (product == null)
                return null;
            
            return product;
        }

        public async Task<DTOs.ProductDTO> AddProduct(CreateProductRequest createProductRequest)
        {
            var warehouse = await _context.Warehouses.FindAsync(createProductRequest.WarehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }
            
            var product = _mapper.Map<Models.Product>(createProductRequest);
            product.Id = Guid.NewGuid();
            product.Prices = new List<Price>();

            foreach (var priceInput in createProductRequest.Prices)
            {
                var destination = await _context.Destinations.FindAsync(priceInput.DestinationId);
                if (destination == null)
                {
                    throw new Exception("Destination not found");
                }
                product.Prices.Add(new Price
                {
                    Id = Guid.NewGuid(),
                    DestinationId = destination.Id,
                    Value = priceInput.Value
                });
            }

            _context.Add(product);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DTOs.ProductDTO>(product);

            return result;
        }

        public async Task<ProductResponse?> UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = await _context.Products.Include(p => p.Prices).FirstOrDefaultAsync(p => p.Id == updateProductRequest.Id);
            if (product == null)
                return null;
            
            product.Name = updateProductRequest.Name;
            product.Prices = _mapper.Map<List<Price>>(updateProductRequest.Prices);
            
            await _context.SaveChangesAsync();
            
            var result = _mapper.Map<ProductResponse>(product);
            
            return result;
        }

        public async Task<DTOs.ProductDTO> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return null;


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<DTOs.ProductDTO>(product);

        }

        public async Task<List<DestinationDTO>> GetDestinations()
        {
            var destinations = await _context.Destinations.ToListAsync();
            var result = _mapper.Map<List<DestinationDTO>>(destinations);
            return result;
        }
    }
}
