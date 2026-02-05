using FlowerShopAPI.Contracts.Warehouses;
using FlowerShopAPI.Managers.Warehouses.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController(IWarehousesManager warehousesManager) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateWarehouse")]
        public async Task<ActionResult<WarehouseDetailsResponse>> CreateWarehouse([FromBody]CreateWarehouseRequest createWarehouseRequest)
        {
            var result = await warehousesManager.CreateWarehouse(createWarehouseRequest);
            return Ok(result);
        }

        [Authorize]
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
        
        [Authorize]
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
        
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
