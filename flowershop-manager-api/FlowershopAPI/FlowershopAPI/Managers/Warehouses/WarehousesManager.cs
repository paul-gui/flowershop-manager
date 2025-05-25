using FlowershopAPI.Data;
using FlowershopAPI.DTOs;
using FlowershopAPI.Managers.Warehouses.Contract;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowershopAPI.Managers.Warehouses
{
    public class WarehousesManager : IWarehousesManager
    {
        ApplicationDbContext _context;
        public WarehousesManager(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(string id)
        {
            var result = await _context.Warehouses.FindAsync(id);
            return result;
        }

        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            var result = await _context.Warehouses.ToListAsync();
            return result;
        }

        public async Task<Warehouse> CreateWarehouseAsync(WarehouseDto dto)
        {
            Warehouse warehouse = new Warehouse()
            {
                Name = dto.Name,
            };
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            return warehouse;
        }


        public async Task<Warehouse> UpdateWarehouseAsync(string id, WarehouseDto dto)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                throw new WarehouseNotFoundException("Warehouse not found!");
            }

            warehouse.Name = dto.Name;

            _context.Update(warehouse);
            await _context.SaveChangesAsync();

            return warehouse;
        }
    }
}
