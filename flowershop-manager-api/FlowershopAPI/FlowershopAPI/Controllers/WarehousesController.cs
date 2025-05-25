using FlowershopAPI.DTOs;
using FlowershopAPI.Managers.Warehouses.Contract;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        IWarehousesManager _warehousesManager;

        public WarehousesController(IWarehousesManager warehousesManager)
        {
            _warehousesManager = warehousesManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Warehouse>>> GetWarehouses()
        {
            var result = await _warehousesManager.GetWarehousesAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouseById(string id)
        {
            var result = await _warehousesManager.GetWarehouseByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Warehouse>> CreateWarehouse([FromBody]WarehouseDto dto)
        {
            var result = await _warehousesManager.CreateWarehouseAsync(dto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Warehouse>> EditWarehouse(string id,  [FromBody]WarehouseDto dto)
        {
            try
            {
                var result = await _warehousesManager.UpdateWarehouseAsync(id, dto);
                return Ok(result);
            }
            catch (WarehouseNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
