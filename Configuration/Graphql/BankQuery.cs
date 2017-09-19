using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Conventions;
using GraphQL.Conventions.Relay;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.Configuration.Graphql
{
    [Name("query")]
    [ImplementViewer(OperationType.Query)]
    public class BankQuery
    {
        [Description("Retrieve customer by its globally unique ID.")]
        public  Task<CustomerGraph> Customer(CustomerContext context, Guid id) =>
            context.GetCustomer(id);
        [Description("Retrieve customers.")]
        public IEnumerable<CustomerGraph> Customers(CustomerContext context) =>
           context.GetCustomers();

    }
}