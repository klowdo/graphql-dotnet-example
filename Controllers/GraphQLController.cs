using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Conventions;
using Microsoft.AspNetCore.Mvc;

namespace graphql_dotnet.Controllers
{
    [Route("graphql")]
    public class GraphQLController: Controller
    {
        public readonly IGraphQLExecutor<ExecutionResult> _executor;
        public GraphQLController(IGraphQLExecutor<ExecutionResult> executor)
        {
            _executor = executor;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Execute(){
            var requestBody = await GetBodyAsync();
            var result = await _executor
                .WithRequest(requestBody)
                .Execute();
            return Ok(result);
        }
        private Task<string> GetBodyAsync()
        {
            using (var receiveStream = Request.Body)
            {
                using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return readStream.ReadToEndAsync();
                }
            }
        }
    }
}