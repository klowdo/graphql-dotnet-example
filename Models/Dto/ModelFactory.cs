using System;
using System.Collections.Generic;
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.Models.Dto
{
    public class ModelFactory
    {
        public static Customer CreateCustomer(string name, string birthdate,string createdby,string[] notes, IEnumerable<Engagement> engagements){
            var newcustomer =  new Customer{
                Id = Guid.NewGuid(),
                Name = name,
                BirthDate = DateTime.Parse(birthdate),
            };
            foreach (var note in notes)
                newcustomer.AddNote(note, createdby);
            foreach (var engangement in engagements)
                newcustomer.AddEngagement(engangement);
            return newcustomer;
        } 
        public static Engagement CreateSavingsEngagement(decimal amount,string name)=>
            CreateEngagement(EngagementType.Savings,amount,name);
        public static Engagement CreateLoanEngagement(decimal amount,string name)=>
            CreateEngagement(EngagementType.Loan,amount,name);
        public static Engagement CreateDeptEngagement(decimal amount,string name)=>
            CreateEngagement(EngagementType.Dept,amount,name);
         private static Engagement CreateEngagement(EngagementType type,decimal amount,string name){
            return new Engagement{
                Id = Guid.NewGuid(),
                Name = name,
                Type = type,
                Amount  = amount
            };
        } 
    }
}