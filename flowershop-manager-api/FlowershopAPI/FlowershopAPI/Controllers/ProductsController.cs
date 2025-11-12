using FlowershopAPI.Contracts.Products;
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

        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var result = await _productsManager.GetProduct(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<DTOs.ProductDTO>> AddProduct([FromBody]CreateProductRequest createProduct)
        {
            var result = await _productsManager.AddProduct(createProduct);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductRequest updateProduct)
        {
            var result = await _productsManager.UpdateProduct(updateProduct);
            
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

        [HttpGet("GetDestinations")]
        public async Task<ActionResult<List<DTOs.DestinationDTO>>> GetDestinations()
        {
            var result = await _productsManager.GetDestinations();
            if (result == null)
            {
                return NotFound();
            } 
            return Ok(result);
        }
    }
}
