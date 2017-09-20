using System;
using System.Threading.Tasks;
using GraphQL.Conventions;
using GraphQL.Conventions.Relay;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Configuration.Graphql.Input;
using graphql_dotnet.Configuration.Graphql.Output;
using graphql_dotnet.Models.Dto;
using graphql_dotnet.services;

namespace graphql_dotnet.Configuration.Graphql
{
    [ImplementViewer(OperationType.Mutation)]
    public class CustomerMutations
    {
         [RelayMutation]
        public async Task<CustomerResult> AddCustomer(
            [Inject] ICustomerService service,
            AddNewCustomerParams input)
        {
            var customerInput = input.newCustomer.Value;
            var customer = new Customer{
                Name = customerInput.newName,
                BirthDate = customerInput.BirthDate,
                Id = customerInput.Id ?? Guid.NewGuid()
            };
            await service.AddCustomerAsync(customer);
            return new CustomerResult
            {
                Customer = new CustomerGraph(customer),
            };
        }





        
        [RelayMutation]
        public async Task<CustomerResult> ChangeName(
            [Inject] ICustomerService service,
            ChangeCustomerNameParams input)
        {
            var customer = await service.UpdateCustomerAsync(input.Properties.Value.CustomerId, c => {
                c.Name = input.Properties.Value.newName;
            });
            return new CustomerResult
            {
                Customer = new CustomerGraph(customer),
            };
        }
    }
}