using FlowershopAPI.Contracts.Sales;
using FlowershopAPI.Managers.Sales.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Controllers;

public class SalesController(ISalesManager salesManager) : ControllerBase
{
    [HttpPost("CreateSale")]
    public async Task<ActionResult> CreateSale(SaleCreationRequest saleCreationRequest)
    {
        var result = await salesManager.CreateSale(saleCreationRequest);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
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