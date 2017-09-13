
using graphql_dotnet.Models.Dto;

namespace graphql_dotnet.Models.Response
{
    public class ResponseFactory
    {
        public static CustomerResponse CreateResponse(Customer customer){
            return new CustomerResponse{
                Id = customer.Id,
                Name = customer.Name,
                BirthDate = customer.BirthDate
            };
        }
        public static NoteResponse CreateResponse(Note note){
            return new NoteResponse{
                Id = note.Id,
                CustomerId = note.CustomerId,
                Text = note.Text,
                CreatedAt = note.CreatedAt,
                CreatedBy = note.CreatedBy
            };
        }
        public static EngagementResponse CreateResponse(Engagement engagement){
            return new EngagementResponse{
                Id = engagement.Id,
                CustomerId = engagement.CustomerId,
                EngagementType = engagement.Type.ToString(),
                Amount = engagement.Amount,
                Name = engagement.Name
            };
        }
    }
}