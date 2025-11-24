using FlowershopAPI.Contracts.Warehouses;
using FlowershopAPI.Managers.Warehouses.Contract;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController(IWarehousesManager warehousesManager) : ControllerBase
    {
        [HttpPost("CreateWarehouse")]
        public async Task<ActionResult<WarehouseDetailsResponse>> CreateWarehouse([FromBody]CreateWarehouseRequest createWarehouseRequest)
        {
            var result = await warehousesManager.CreateWarehouse(createWarehouseRequest);
            return Ok(result);
        }

        [HttpGet("GetWarehouses")]
        public async Task<ActionResult<List<WarehouseResponse>>> GetWarehouses()
        {
            var result = await warehousesManager.GetWarehouses();
            if (result.Count == 0)
            {
                return NotFound("No warehouses found");
            }
            return Ok(result);
        }
        
        [HttpGet("GetWarehouse/{id}")]
        public async Task<ActionResult<WarehouseDetailsResponse>> GetWarehouse(Guid id)
        {
            var result = await warehousesManager.GetWarehouseById(id);
            if (result == null)
            {
                return NotFound("Warehouse not found");
            }
            return Ok(result);
        }
        
        [HttpPut("UpdateWarehouse")]
        public async Task<ActionResult<WarehouseDetailsResponse>> UpdateWarehouse([FromBody]UpdateWarehouseRequest updateWarehouseRequest)
        {
            var result = await warehousesManager.UpdateWarehouse(updateWarehouseRequest);
            if (result == null)
            {
                return NotFound("Warehouse not found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteWarehouse/{id}")]
        public async Task<ActionResult<WarehouseDetailsResponse>> DeleteWarehouse(Guid id)
        {
            var result = await warehousesManager.DeleteWarehouse(id);
            if (result == null)
            {
                return NotFound("Warehouse not found");
            }
            return Ok(result);
        }
    }
}
