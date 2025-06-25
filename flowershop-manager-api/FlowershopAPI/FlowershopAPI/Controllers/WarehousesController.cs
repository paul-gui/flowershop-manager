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

        [HttpGet("GetWarehouses")]
        public async Task<ActionResult<List<DTOs.WarehouseDTO>>> GetWarehouses()
        {
            var result = await _warehousesManager.GetWarehousesAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("GetWarehouse/{id}")]
        public async Task<ActionResult<DTOs.WarehouseDTO>> GetWarehouse(Guid id)
        {
            var result = await _warehousesManager.GetWarehouseByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("CreateWarehouse")]
        public async Task<ActionResult<DTOs.WarehouseDTO>> CreateWarehouse([FromBody]WarehouseInput dto)
        {
            var result = await _warehousesManager.CreateWarehouseAsync(dto);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut("EditWarehouse/{id}")]
        public async Task<ActionResult<DTOs.WarehouseDTO>> EditWarehouse(Guid id,  [FromBody]WarehouseInput dto)
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

        [HttpDelete("DeleteWarehouse/{id}")]
        public async Task<ActionResult<DTOs.WarehouseDTO>> DeleteWarehouse(Guid id)
        {
            try
            {
                var result = await _warehousesManager.DeleteWarehouseAsync(id);
                return Ok(result);
            }
            catch (WarehouseNotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "An unexpected error occured.");
            }

        }
    }
}
