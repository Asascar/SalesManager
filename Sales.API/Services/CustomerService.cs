using Sales.API.Models;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Services
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(string name, string email, string phone)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Customer name is required.");

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone
            };

            await _customerRepository.Add(customer);
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(int skip = 0, int take = 0)
        {
            return await _customerRepository.Get(skip, take);
        }

        public async Task<Customer> UpdateCustomerAsync(Guid customerId, string name, string email, string phone)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Customer name is required.");

            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            customer.Name = name;
            customer.Email = email;
            customer.Phone = phone;

            _customerRepository.Update(customer);
            return customer;
        }

        // Deletar cliente
        public async Task DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            _customerRepository.Delete(customer);
        }
    }

}
