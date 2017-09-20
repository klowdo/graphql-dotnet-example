using System;
using GraphQL.Conventions;
using GraphQL.Conventions.Relay;

namespace graphql_dotnet.Configuration.Graphql.Input
{
    public class AddNewCustomerParams : IRelayMutationInputObject
    {
        public string ClientMutationId { get ; set; }
        [Description("Customer  input")]
        public NonNull<AddCustomerInput> newCustomer { get; set; }
        [InputType]
        [Description("Customer inpur")]
        public class AddCustomerInput {
            [Description("The customer id ")]
            public Guid? Id { get; set; }
            [Description("The new name")]
            public NonNull<string> newName { get; set; }
            [Description("The customers birthdate")]
            public DateTime BirthDate { get; set; }
        }
    }
}