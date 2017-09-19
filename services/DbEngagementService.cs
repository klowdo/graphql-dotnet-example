using System;
using System.Collections.Generic;
using System.Linq;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.services
{
    public class DbEngagementService : IEngagementService
    {
        private readonly BigBadBankContext _context;
        public DbEngagementService(BigBadBankContext context)
        {
            _context = context;

        }
        public IEnumerable<Engagement> GetEngagementsByCustomerid(Guid id)
        {
           return _context.Engagements.Where(x => x.CustomerId == id);
        }
    }
}