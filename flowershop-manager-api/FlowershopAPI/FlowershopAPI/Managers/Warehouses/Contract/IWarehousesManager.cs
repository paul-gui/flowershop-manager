using FlowershopAPI.DTOs;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Managers.Warehouses.Contract
{
    public interface IWarehousesManager
    {
        Task<List<DTOs.WarehouseDTO>> GetWarehousesAsync();
        Task<DTOs.WarehouseDTO?> GetWarehouseByIdAsync(Guid id);
        Task<DTOs.WarehouseDTO> CreateWarehouseAsync(WarehouseInput warehouseDto);
        Task<DTOs.WarehouseDTO> UpdateWarehouseAsync(Guid id, WarehouseInput dto);
        Task<DTOs.WarehouseDTO> DeleteWarehouseAsync(Guid id);
    }
}
