using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Conventions;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Configuration.Graphql;
using graphql_dotnet.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace graphql_dotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var executor = GraphQLEngine.New<Configuration.Graphql.BankQuery,CustomerMutations>()
                        .NewExecutor();
            services.AddDbContext<BigBadBankContext>(options => options.UseSqlite("Data Source=customers.db"));
            services.AddTransient<IDependencyInjector,GraphQLDependencyInjector>();
            services.AddTransient<IUserContext,CustomerContext>();
            services.AddTransient<ICustomerService, DbCustomerService>();
            services.AddTransient<INoteService, DbNoteServise>();
            services.AddTransient<IEngagementService, DbEngagementService>();
            services.AddTransient<IGraphQLExecutor<ExecutionResult>>(provider => 
                        executor
                        .EnableProfiling()
                        .WithUserContext(provider.GetService<IUserContext>())
                        .WithDependencyInjector(provider.GetService<IDependencyInjector>()));
                  

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseGraphiQl();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
