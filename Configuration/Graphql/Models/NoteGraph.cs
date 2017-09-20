using System;
using System.Threading.Tasks;
using GraphQL.Conventions;
using graphql_dotnet.Models.Dto;
using graphql_dotnet.services;

namespace graphql_dotnet.Configuration.Graphql.Models
{
    [Name("note")]
    [Description("A customer note")]
    public class NoteGraph
    {
        private readonly Note _dto;
        public NoteGraph(Note dto)
        {
            _dto = dto;

        }
        [Description("Unique note identifier.")]
        public Guid Id => _dto.Id;
        [Description("Note text.")]
        public string Text => _dto.Text;
        [Description("Note Creator.")]
        public string CreatedBy => _dto.CreatedBy;
        [Description("Note creation date.")]
        public DateTime CreatedAt => _dto.CreatedAt;
        [Description("Notes customer")]
         public Task<CustomerGraph> Customer([Inject] CustomerContext service) => service.GetCustomer(_dto.CustomerId);
    }
}