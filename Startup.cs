using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.GraphQL;
using GraphQL;
using grphql_test.Entities;
using grphql_test.GraphQL;
using grphql_test.QueryBuilders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace grphql_test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddDapperGraphQL(options =>
            {
                // Add GraphQL types
                options.AddType<CompanyType>();
                options.AddType<EmailType>();
                options.AddType<PersonType>();
                options.AddType<GraphQL.PhoneType>();
                options.AddType<PersonQuery>();
                options.AddType<PersonMutation>();
                options.AddType<PersonInputType>();

                // Add the GraphQL schema
                options.AddSchema<PersonSchema>();

                // Add query builders for dapper
                options.AddQueryBuilder<Company, CompanyQueryBuilder>();
                options.AddQueryBuilder<Email, EmailQueryBuilder>();
                options.AddQueryBuilder<Person, PersonQueryBuilder>();
                options.AddQueryBuilder<Phone, PhoneQueryBuilder>();
            });

            services.AddTransient<IDbConnection>(serviceProvider => GetDbConnection());
        }

        private IDbConnection GetDbConnection()
        {
            return 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(op => op
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            app.UseMvc();
        }
    }
}
