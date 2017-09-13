using System;
using System.Collections.Generic;
using System.Linq;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.Configuration.data
{
    public class DbInitializer
    {
         public static void Initialize(BigBadBankContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }
            context.Add(ModelFactory.CreateCustomer(
                name:"Felix Svensson",
                birthdate:"1995-09-17 7:34:42Z",
                createdby:"system",
                notes:new []{
                    "bra kund",
                    "betalar i tid",
                    "spara mycket pengar"
                },
                engagements: new[]{
                    ModelFactory.CreateSavingsEngagement(20000, "Nya cykel kontot"),
                    ModelFactory.CreateDeptEngagement(20000, "Monster kylen"),
                }
            ));
            context.Add(ModelFactory.CreateCustomer(
                name:"Daniel Praktikant",
                birthdate:"1985-09-18 7:34:42Z",
                createdby:"system",
                notes:new []{
                    "bra kund",
                    "betalar sällan i tid",
                    "lånar mycket pengar"
                },
                engagements: new[]{
                    ModelFactory.CreateSavingsEngagement(100000, "Bröllop"),
                    ModelFactory.CreateLoanEngagement(3000000, "Bröllop..."),
                }
            ));
            context.Add(ModelFactory.CreateCustomer(
                name:"Mats Pokerproffs",
                birthdate:"1982-07-08 7:34:42Z",
                createdby:"system",
                notes:new []{
                    "bra kund",
                    "betalar aldrig i tid",
                    "sparar för mycket pengar"
                },
                 engagements: new[]{
                    ModelFactory.CreateSavingsEngagement(100000, "Poker pengar"),
                    ModelFactory.CreateDeptEngagement(3000000, "Azure..."),
                }
            ));
            context.SaveChanges();
           
        }
    }
}