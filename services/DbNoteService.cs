using System;
using System.Collections.Generic;
using System.Linq;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.services
{
    public class DbNoteServise:INoteService
    {
        private readonly BigBadBankContext _context;
        public DbNoteServise(BigBadBankContext context)
        {
            _context = context;

        }
        public IEnumerable<Note> GetNotesByCustomerId(Guid id)
        {
           return _context.Notes.Where(x => x.CustomerId == id);
        }
    }
}