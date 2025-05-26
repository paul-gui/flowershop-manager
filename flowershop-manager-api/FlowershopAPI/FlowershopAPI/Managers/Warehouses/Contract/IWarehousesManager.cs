using FlowershopAPI.DTOs;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Managers.Warehouses.Contract
{
    public interface IWarehousesManager
    {
        Task<List<Warehouse>> GetWarehousesAsync();
        Task<Warehouse?> GetWarehouseByIdAsync(string id);
        Task<Warehouse> CreateWarehouseAsync(WarehouseDto warehouseDto);
        Task<Warehouse> UpdateWarehouseAsync(string id, WarehouseDto dto);
    }
}
