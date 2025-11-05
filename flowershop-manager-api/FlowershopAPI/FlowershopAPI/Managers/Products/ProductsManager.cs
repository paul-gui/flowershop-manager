using AutoMapper;
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

        public Task<DTOs.ProductDTO> GetProduct(Guid Id)
        {
            
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

        public Task<ProductDTO> UpdateProduct(Guid Id, CreateProductRequest createProduct)
        {
            throw new NotImplementedException();
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
