using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.services
{
    public class DbCustomerService : ICustomerService
    {
        private readonly BigBadBankContext _context;
        public DbCustomerService(BigBadBankContext context) => _context = context;

        public  Task AddCustomerAsync(Customer newCustomer)
        {
             _context.Customers.AddAsync(newCustomer);
            return _context.SaveChangesAsync();
        }

        public Customer GetCustomerById(Guid id) =>
            _context.Customers.Find(id);
        public Task<Customer> GetCustomerByIdAsync(Guid id) => 
            _context.Customers.FindAsync(id);

        public Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return _context.SaveChangesAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(Guid id, Action<Customer> update)
        {
            var customer = await GetCustomerByIdAsync(id);
            update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}