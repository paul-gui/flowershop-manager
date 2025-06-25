using AutoMapper;
using FlowershopAPI.Data;
using FlowershopAPI.DTOs;
using FlowershopAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException();
        }

        public async Task<DTOs.ProductDTO> AddProduct(DTOs.ProductInput productDto)
        {
            //Product product = new Product
            //{
            //    Name = productDto.Name,
            //    WarehouseId = productDto.WarehouseId,
            //};

            var product = _mapper.Map<Models.Product>(productDto);

            _context.Add(product);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DTOs.ProductDTO>(product);

            return result;
        }

        public Task<DTOs.ProductDTO> UpdateProduct(Guid Id, DTOs.ProductInput product)
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
    }
}
