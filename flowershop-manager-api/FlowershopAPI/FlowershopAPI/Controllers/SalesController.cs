using System.Security.Claims;
using FlowerShopAPI.Contracts.Sales;
using FlowerShopAPI.Managers.Sales.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController(ISalesManager salesManager) : ControllerBase
{
    [Authorize]
    [HttpPost("CreateSale")]
    public async Task<ActionResult> CreateSale(SaleCreationRequest saleCreationRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");
        if (userId == null)
        {
            return Unauthorized();
        }
        
        var result = await salesManager.CreateSale(userId, saleCreationRequest);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetSales")]
    public async Task<ActionResult> GetSales([FromQuery] SalesFilterRequest salesFilterRequest)
    {
        var result = await salesManager.GetSales(salesFilterRequest);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetSaleForEdit/{saleId}")]
    public async Task<ActionResult<SaleResponse>> GetSaleForEdit(Guid saleId)
    {
        var result = await salesManager.GetSaleForEdit(saleId);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateSale/{saleId}")]
    public async Task<ActionResult> UpdateSale(Guid saleId, [FromBody]SaleUpdateRequest saleUpdateRequest)
    {
        var result = await salesManager.UpdateSale(saleId, saleUpdateRequest);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteSale/{saleId}")]
    public async Task<ActionResult> DeleteSale(Guid saleId)
    {
        var result = await salesManager.DeleteSale(saleId);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
}