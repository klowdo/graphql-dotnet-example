using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Conventions;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace graphql_dotnet.Configuration.Graphql
{
    public class CustomerContext : IUserContext, IDataLoaderContextProvider
    {
        private readonly BigBadBankContext _context;
        public CustomerContext(BigBadBankContext context)
        {
            _context = context;
        }
        public async Task<CustomerGraph> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return new CustomerGraph(customer);
        }
        public IEnumerable<CustomerGraph> GetCustomers(){
            var customers = _context.Customers
                .Select(x => new CustomerGraph(x));
            return customers;
        }

        public Task FetchData(CancellationToken token) => Task.CompletedTask;
    }
}