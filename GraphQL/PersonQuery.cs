﻿using Dapper.GraphQL;
using GraphQL.Types;
using grphql_test.Entities;
using grphql_test.EntityMappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.GraphQL
{
    public class PersonQuery :
        ObjectGraphType
    {
        public PersonQuery(
            IQueryBuilder<Person> personQueryBuilder,
            IServiceProvider serviceProvider)
        {
            Field<ListGraphType<PersonType>>(
                "people",
                description: "A list of people.",
                resolve: context =>
                {
                    var alias = "person";
                    var query = SqlBuilder
                        .From<Person>(alias)
                        .OrderBy($"{alias}.Id");
                    query = personQueryBuilder.Build(query, context.FieldAst, alias);

                    // Create a mapper that understands how to uniquely identify the 'Person' class,
                    // and will deduplicate people as they pass through it
                    var personMapper = new PersonEntityMapper();

                    using (var connection = serviceProvider.GetRequiredService<IDbConnection>())
                    {
                        var results = query
                            .Execute(connection, context.FieldAst, personMapper)
                            .Distinct();
                        return results;
                    }
                }
            );

            FieldAsync<ListGraphType<PersonType>>(
                "peopleAsync",
                description: "A list of people fetched asynchronously.",
                resolve: async context =>
                {
                    var alias = "person";
                    var query = SqlBuilder
                        .From($"Person {alias}")
                        .OrderBy($"{alias}.Id");
                    query = personQueryBuilder.Build(query, context.FieldAst, alias);

                    // Create a mapper that understands how to uniquely identify the 'Person' class,
                    // and will deduplicate people as they pass through it
                    var personMapper = new PersonEntityMapper();

                    using (var connection = serviceProvider.GetRequiredService<IDbConnection>())
                    {
                        connection.Open();

                        var results = await query.ExecuteAsync(connection, context.FieldAst, personMapper);
                        return results.Distinct();
                    }
                }
            );

            Field<PersonType>(
                "person",
                description: "Gets a person by ID.",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id", Description = "The ID of the person." }
                ),
                resolve: context =>
                {
                    var id = context.Arguments["id"];
                    var alias = "person";
                    var query = SqlBuilder
                        .From($"Person {alias}")
                        .Where($"{alias}.Id = @id", new { id })
                        // Even though we're only getting a single person, the process of deduplication
                        // may return several entities, so we sort by ID here for consistency
                        // with test results.
                        .OrderBy($"{alias}.Id");

                    query = personQueryBuilder.Build(query, context.FieldAst, alias);

                    // Create a mapper that understands how to uniquely identify the 'Person' class,
                    // and will deduplicate people as they pass through it
                    var personMapper = new PersonEntityMapper();

                    using (var connection = serviceProvider.GetRequiredService<IDbConnection>())
                    {
                        var results = query
                            .Execute(connection, context.FieldAst, personMapper)
                            .Distinct();
                        return results.FirstOrDefault();
                    }
                }
            );
        }
    }
}
