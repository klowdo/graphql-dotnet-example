using System;

namespace graphql_dotnet.Models.Response
{
    public class EngagementResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string EngagementType { get; set; }
    }
}