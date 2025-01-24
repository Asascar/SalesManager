using Microsoft.AspNetCore.Mvc;
using Sales.API.DTO.Product.Request;
using Sales.API.Services;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var product = await _productService.CreateProductAsync(request.Name, request.Description, request.Price);
                return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            var products = await _productService.GetAllProductsAsync(skip, take);
            return Ok(products);
        }

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(productId);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(productId, request.Name, request.Description, request.Price);
                return Ok(updatedProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                await _productService.DeleteProductAsync(productId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }
    }
}
