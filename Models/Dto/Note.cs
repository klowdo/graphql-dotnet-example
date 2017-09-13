using System;

namespace graphql_dotnet.Models.Dto
{
    public class Note: Entity<Guid>
    {
        public Guid CustomerId { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}