using System;

namespace graphql_dotnet.Models.Response
{
    public class NoteResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}