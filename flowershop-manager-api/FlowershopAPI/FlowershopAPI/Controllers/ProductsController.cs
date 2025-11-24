using FlowershopAPI.Contracts.Destinations;
using FlowershopAPI.Contracts.Products;
using FlowershopAPI.Managers.Products;
using FlowershopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowershopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsManager productsManager) : ControllerBase
    {
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody]CreateProductRequest createProduct)
        {
            var result = await productsManager.CreateProduct(createProduct);
            
            return Ok(result);
        }
        
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var result = await productsManager.GetProduct(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }
        
        [HttpGet("GetDestinations")]
        public async Task<ActionResult<List<DestinationResponse>>> GetDestinations()
        {
            var result = await productsManager.GetDestinations();
            if (result.Count == 0)
            {
                return NotFound("No destinations found");
            } 
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductRequest updateProduct)
        {
            var result = await productsManager.UpdateProduct(updateProduct);
            
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id)
        {
            var result = await productsManager.DeleteProduct(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
