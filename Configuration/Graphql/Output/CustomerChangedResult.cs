using GraphQL.Conventions;
using GraphQL.Conventions.Relay;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.Configuration.Graphql.Output
{
    public class CustomerResult : IRelayMutationOutputObject
    {
        public string ClientMutationId { get; set; }
        [Description("The customer changed.")]
        public CustomerGraph Customer { get; set; }
    }
}