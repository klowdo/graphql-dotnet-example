using graphql_dotnet.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace graphql_dotnet.Configuration.data
{
    public class BigBadBankContext: DbContext, IBigBadDataContext
    {
        public BigBadBankContext(DbContextOptions<BigBadBankContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get ; set ; }
    }
}