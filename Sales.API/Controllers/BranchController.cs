using Microsoft.AspNetCore.Mvc;
using Sales.API.DTO.Branch.Request;
using Sales.API.Services;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly BranchService _branchService;

        public BranchController(BranchService branchService)
        {
            _branchService = branchService;
        }

        // Criar uma nova filial
        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var branch = await _branchService.CreateBranchAsync(request.Name, request.Location);
                return CreatedAtAction(nameof(GetBranchById), new { branchId = branch.Id }, branch);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Recuperar todas as filiais
        [HttpGet]
        public async Task<IActionResult> GetAllBranches([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            var branches = await _branchService.GetAllBranchesAsync(skip, take);
            return Ok(branches);
        }

        // Recuperar uma filial pelo ID
        [HttpGet("{branchId:guid}")]
        public async Task<IActionResult> GetBranchById(Guid branchId)
        {
            try
            {
                var branch = await _branchService.GetBranchByIdAsync(branchId);
                return Ok(branch);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Branch not found.");
            }
        }

        // Atualizar uma filial existente
        [HttpPut("{branchId:guid}")]
        public async Task<IActionResult> UpdateBranch(Guid branchId, [FromBody] UpdateBranchRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var updatedBranch = await _branchService.UpdateBranchAsync(branchId, request.Name, request.Location);
                return Ok(updatedBranch);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Branch not found.");
            }
        }

        // Deletar uma filial
        [HttpDelete("{branchId:guid}")]
        public async Task<IActionResult> DeleteBranch(Guid branchId)
        {
            try
            {
                await _branchService.DeleteBranchAsync(branchId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Branch not found.");
            }
        }
    }
}
