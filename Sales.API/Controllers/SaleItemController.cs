using Microsoft.AspNetCore.Mvc;
using Sales.API.DTO.SaleItem.Request;
using Sales.API.Services;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleItemController : ControllerBase
    {
        private readonly SaleItemService _saleItemService;

        public SaleItemController(SaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }

        // Adicionar item a uma venda
        [HttpPost]
        public async Task<IActionResult> AddSaleItem([FromBody] AddSaleItemRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");

            if (request.Quantity < 1 || request.Quantity > 20)
                return BadRequest("Quantity must be between 1 and 20.");

            try
            {
                var saleItem = await _saleItemService.AddItemToSaleAsync(
                    request.SaleId,
                    request.ProductId,
                    request.Quantity,
                    request.UnitPrice
                );

                return CreatedAtAction(nameof(GetSaleItemById), new { saleItemId = saleItem.Id }, saleItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obter item por ID
        [HttpGet("{saleItemId:guid}")]
        public async Task<IActionResult> GetSaleItemById(Guid saleItemId)
        {
            try
            {
                var saleItem = await _saleItemService.GetItemByIdAsync(saleItemId);
                return Ok(saleItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale item not found.");
            }
        }

        // Atualizar item de venda
        [HttpPut("{saleItemId:guid}")]
        public async Task<IActionResult> UpdateSaleItem(Guid saleItemId, [FromBody] UpdateSaleItemRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");

            if (request.Quantity < 1 || request.Quantity > 20)
                return BadRequest("Quantity must be between 1 and 20.");

            try
            {
                var updatedItem = await _saleItemService.UpdateItemAsync(
                    saleItemId,
                    request.Quantity,
                    request.UnitPrice
                );

                return Ok(updatedItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale item not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deletar item de venda
        [HttpDelete("{saleItemId:guid}")]
        public async Task<IActionResult> DeleteSaleItem(Guid saleItemId)
        {
            try
            {
                await _saleItemService.DeleteItemAsync(saleItemId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale item not found.");
            }
        }
    }

}
