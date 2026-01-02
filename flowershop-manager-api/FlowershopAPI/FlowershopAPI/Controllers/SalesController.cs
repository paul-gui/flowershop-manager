using System.Security.Claims;
using FlowershopAPI.Contracts.Sales;
using FlowershopAPI.Managers.Sales.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController(ISalesManager salesManager) : ControllerBase
{
    [HttpPost("CreateSale")]
    public async Task<ActionResult> CreateSale(SaleCreationRequest saleCreationRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");
        if (userId == null)
        {
            return Forbid();
        }
        
        var result = await salesManager.CreateSale(userId, saleCreationRequest);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }

    [HttpGet("GetSales")]
    public async Task<ActionResult> GetSales([FromQuery] SalesFilterRequest salesFilterRequest)
    {
        var result = await salesManager.GetSales(salesFilterRequest);
        return Ok(result);
    }

    [HttpGet("GetSale")]
    public async Task<ActionResult<SaleResponse>> GetSale()
    {
        return NoContent();
    }

    [HttpPut("UpdateSale")]
    public async Task<ActionResult> UpdateSale()
    {
        return NoContent();
    }

    [HttpDelete("DeleteSale")]
    public async Task<ActionResult> DeleteSale()
    {
        return NoContent();
    }
}