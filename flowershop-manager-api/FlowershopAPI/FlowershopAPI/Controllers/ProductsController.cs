using FlowershopAPI.DTOs;
using FlowershopAPI.Managers.Products;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsManager _productsManager;
        public ProductsController(IProductsManager productsManager)
        {
            _productsManager = productsManager;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<DTOs.ProductDTO>> AddProduct([FromBody]DTOs.ProductInput product)
        {
            var result = await _productsManager.AddProduct(product);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<DTOs.ProductDTO>> DeleteProduct(Guid id)
        {
            var result = await _productsManager.DeleteProduct(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
