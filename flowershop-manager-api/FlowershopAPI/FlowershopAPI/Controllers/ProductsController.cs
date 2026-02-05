using FlowerShopAPI.Contracts.Destinations;
using FlowerShopAPI.Contracts.Products;
using FlowerShopAPI.Managers.Products.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsManager productsManager) : ControllerBase
    {
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody]CreateProductRequest createProduct)
        {
            var result = await productsManager.CreateProduct(createProduct);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok(result.Data);
        }
        
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var result = await productsManager.GetProduct(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpGet("GetProductsByWarehouseId/{warehouseId:guid}")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductsByWarehouseId(Guid warehouseId)
        {
            var result = await productsManager.GetProductsByWarehouseId(warehouseId);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok(result.Data);
        }

        [HttpGet("GetPrice/{productId:guid}/{destinationId:guid}")]
        public async Task<ActionResult<decimal>> GetPrice(Guid productId, Guid destinationId)
        {
            var result = await productsManager.GetPrice(productId, destinationId);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok(result.Data);
        }
        
        [HttpGet("GetDestinations")]
        public async Task<ActionResult<List<DestinationResponse>>> GetDestinations()
        {
            var result = await productsManager.GetDestinations();
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            } 
            return Ok(result.Data);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductRequest updateProduct)
        {
            var result = await productsManager.UpdateProduct(updateProduct);
            
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id)
        {
            var result = await productsManager.DeleteProduct(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
