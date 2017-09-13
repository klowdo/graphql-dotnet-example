using System;

namespace graphql_dotnet.Models.Dto
{
    public class Engagement: Entity<Guid>
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public EngagementType Type { get; set; }
    }
   public enum EngagementType
   {
       Savings,
       Loan,
       Dept
   }
}