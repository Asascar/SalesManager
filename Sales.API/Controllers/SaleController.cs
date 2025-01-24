namespace Sales.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sales.API.DTO.Sale.Request;
    using Sales.API.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SalesService _saleService;

        public SaleController(SalesService saleService)
        {
            _saleService = saleService;
        }

        // Criar uma nova venda
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            if (request == null || !request.Items.Any())
                return BadRequest("A sale must have at least one item.");

            try
            {
                var sale = await _saleService.CreateSaleAsync(request);
                return CreatedAtAction(nameof(GetSaleById), new { saleId = sale.Id }, sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Buscar todas as vendas
        [HttpGet]
        public async Task<IActionResult> GetAllSales([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            var sales = await _saleService.GetAllSalesAsync(skip, take);
            return Ok(sales);
        }

        // Buscar venda por ID
        [HttpGet("{saleId:guid}")]
        public async Task<IActionResult> GetSaleById(Guid saleId)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(saleId);
                return Ok(sale);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale not found.");
            }
        }

        // Atualizar uma venda
        [HttpPut("{saleId:guid}")]
        public async Task<IActionResult> UpdateSale(Guid saleId, [FromBody] UpdateSaleRequest request)
        {
            if (request == null || !request.Items.Any())
                return BadRequest("A sale must have at least one item.");

            try
            {
                await _saleService.CancelSaleAsync(saleId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Cancelar uma venda
        [HttpPut("{saleId:guid}/cancel")]
        public async Task<IActionResult> CancelSale(Guid saleId)
        {
            try
            {
                await _saleService.CancelSaleAsync(saleId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deletar uma venda
        [HttpDelete("{saleId:guid}")]
        public async Task<IActionResult> DeleteSale(Guid saleId)
        {
            try
            {
                await _saleService.DeleteSale(saleId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Sale not found.");
            }
        }
    }

}
