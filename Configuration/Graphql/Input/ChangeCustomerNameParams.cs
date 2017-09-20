using System;
using GraphQL.Conventions;
using GraphQL.Conventions.Relay;

namespace graphql_dotnet.Configuration.Graphql.Input
{
    [Description("Operation for changing a name of customer.")]
    public class ChangeCustomerNameParams : IRelayMutationInputObject
    {
        public string ClientMutationId { get; set; }
        [Description("Customer name input")]
        public NonNull<ChangeCustomerNameInput> Properties { get; set; }
        [InputType]
        [Description("a name")]
        public class ChangeCustomerNameInput {
            [Description("The customer id to change name on.")]
            public Guid CustomerId { get; set; }
            [Description("The new name")]
            public NonNull<string> newName { get; set; }
        }
    }
}