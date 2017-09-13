using graphql_dotnet.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace graphql_dotnet.Configuration.data
{
    public interface IBigBadDataContext
    {
         DbSet<Customer> Customers {get; set;}
    }
}