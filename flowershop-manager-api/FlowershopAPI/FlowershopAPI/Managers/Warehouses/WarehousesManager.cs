using AutoMapper;
using FlowershopAPI.Data;
using FlowershopAPI.DTOs;
using FlowershopAPI.Managers.Warehouses.Contract;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowershopAPI.Managers.Warehouses
{
    public class WarehousesManager : IWarehousesManager
    {
        ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public WarehousesManager(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DTOs.WarehouseDTO?> GetWarehouseByIdAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.Include(w => w.Products).FirstOrDefaultAsync(w => w.Id == id);
            var result = _mapper.Map<DTOs.WarehouseDTO>(warehouse);

            return result;
        }

        public async Task<List<DTOs.WarehouseDTO>> GetWarehousesAsync()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            var result = _mapper.Map<List<DTOs.WarehouseDTO>>(warehouses);

            return result;
        }

        public async Task<DTOs.WarehouseDTO> CreateWarehouseAsync(WarehouseInput dto)
        {
            
            var warehouse = _mapper.Map<Models.Warehouse>(dto);
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DTOs.WarehouseDTO>(warehouse);

            return result;
        }


        public async Task<DTOs.WarehouseDTO> UpdateWarehouseAsync(Guid id, WarehouseInput dto)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                throw new WarehouseNotFoundException("Warehouse not found!");
            }

            warehouse.Name = dto.Name;

            _context.Update(warehouse);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DTOs.WarehouseDTO>(warehouse);

            return result;
        }

        public async Task<DTOs.WarehouseDTO> DeleteWarehouseAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            if(warehouse == null)
            {
                throw new WarehouseNotFoundException("Warehouse not found!");
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DTOs.WarehouseDTO>(warehouse);
            return result;
        }
    }
}
