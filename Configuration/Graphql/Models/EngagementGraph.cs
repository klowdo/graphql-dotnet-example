using System.Threading.Tasks;
using GraphQL.Conventions;
using graphql_dotnet.Models.Dto;
using System;

namespace graphql_dotnet.Configuration.Graphql.Models
{
    [Name("Engagement")]
    [Description("A engagement of a customer")]
    public class EngagementGraph
    {
        private readonly Engagement _dto ;
        public EngagementGraph(Engagement dto)
        {
            _dto = dto;
        }
        [Description("Unique engagement identifier.")]
        public Guid Id => _dto.Id;

        [Description("Engagement name")]
        public string Name => _dto.Name;
        [Description("Engagement amount")]
        public decimal Amount => _dto.Amount;
        [Description("Engagement type")]
        public string Type => _dto.Type.ToString();
        [Description("Engagement customer")]
        public Task<CustomerGraph> Customer([Inject] CustomerContext service) => service.GetCustomer(_dto.CustomerId);

    }
}