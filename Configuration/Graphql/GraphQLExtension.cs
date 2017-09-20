using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Conventions;
using GraphQL.Conventions.Web;
using GraphQL.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace graphql_dotnet.Configuration.Graphql
{
    public static class GraphQLExtension
    {
         public static IServiceCollection AddGraphQL<TQuery,TMutation>(this IServiceCollection services)
        {
            services.AddTransient<IGraphQLExecutor<ExecutionResult>>(provider => 
                        GraphQLEngine.New<TQuery,TMutation>()
                        .NewExecutor()
                        .WithUserContext(provider.GetService<IUserContext>())
                        .WithDependencyInjector(provider.GetService<IDependencyInjector>()));
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            return services;
        }

        public static IApplicationBuilder UseGraphQLEndPoint(this IApplicationBuilder builder,
            string path = "/graphql")
        {
            builder.Use(async (context, next) =>
                {
                    if (context.Request.Path == path)
                    {
                            var documentWriter = context.RequestServices.GetRequiredService<IDocumentWriter>();
                            var executor = context.RequestServices.GetRequiredService<IGraphQLExecutor<ExecutionResult>>();
                            var query = await GetQueryAsync(context);
                            var result = await executor.WithRequest(query)
                                    .Execute();

                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/json";
                            await WriteResponseJson(context.Response.Body, result, documentWriter);
                    }
                    else
                    {
                        await next();
                    }   
                }
            );

            return builder;
        }
        private static async Task WriteResponseJson(Stream responseBody, ExecutionResult result, IDocumentWriter documentWriter)
        {
            var json = documentWriter.Write(result);

            using (var streamWriter = new StreamWriter(responseBody, System.Text.Encoding.UTF8, 4069, true))
            {
                await streamWriter.WriteAsync(json);
                await streamWriter.FlushAsync();
            }
        }

        private static  Task<string> GetQueryAsync(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                return reader.ReadToEndAsync();
            }
        }
    }
}