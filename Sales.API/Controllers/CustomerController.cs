namespace Sales.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sales.API.DTO.Customer.Request;
    using Sales.API.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // Criar um novo cliente
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var customer = await _customerService.CreateCustomerAsync(request.Name, request.Email, request.PhoneNumber);
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, customer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Recuperar todos os clientes
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            var customers = await _customerService.GetAllCustomersAsync(skip, take);
            return Ok(customers);
        }

        // Recuperar um cliente pelo ID
        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);
                return Ok(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found.");
            }
        }

        // Atualizar um cliente existente
        [HttpPut("{customerId:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, request.Name, request.Email, request.PhoneNumber);
                return Ok(updatedCustomer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found.");
            }
        }

        // Deletar um cliente
        [HttpDelete("{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found.");
            }
        }
    }

}
