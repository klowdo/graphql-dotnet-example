using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Conventions;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Configuration.Graphql.Models;
using graphql_dotnet.services;

namespace graphql_dotnet.Models.Dto
{
    [Name("customer")]
    [Description("A customer")]
    public class CustomerGraph
    {
        private readonly Customer _dto;
        public CustomerGraph(Customer dto)
        {
            _dto = dto;
        }
        [Description("Unique customer identifier.")]
        public Guid Id => _dto.Id;
        [Description("Name of the Customer")]
        public string Name => _dto.Name;
        [Description("BirthDate of the Customer")]
        public DateTime BirthDate => _dto.BirthDate;
        [Description("The notes of the customer.")]
        public  IEnumerable<NonNull<NoteGraph>> Notes([Inject] INoteService  service)
        {
            if (_dto?.Id != null)
            {
                var notes = service.GetNotesByCustomerId(_dto.Id);
                foreach (var note in notes)
                {
                    yield return new NoteGraph(note);
                }
            }
        }
        public  IEnumerable<NonNull<EngagementGraph>> Engagements([Inject] IEngagementService  service)
        {
            if (_dto?.Id != null)
            {
                var engagements = service.GetEngagementsByCustomerid(_dto.Id);
                foreach (var engagement in engagements)
                {
                    yield return new EngagementGraph(engagement);
                }
            }
        }
    }
}