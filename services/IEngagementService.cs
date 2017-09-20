using System;
using System.Collections.Generic;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.services
{
    public interface IEngagementService
    {
         IEnumerable<Engagement> GetEngagementsByCustomerid(Guid id);
    }
}