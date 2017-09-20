using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.services
{
    public interface ICustomerService
    {
         Customer GetCustomerById(Guid id);
         Task<Customer> GetCustomerByIdAsync(Guid id);
         Task UpdateCustomerAsync(Customer customer);
         Task AddCustomerAsync(Customer newCustomer);
        Task<Customer> UpdateCustomerAsync(Guid id, Action<Customer> update);
         
    }
}