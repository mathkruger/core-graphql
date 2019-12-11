using Dapper.GraphQL;
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
    public class PersonMutation : ObjectGraphType
    {
        public PersonMutation(IQueryBuilder<Person> personQueryBuilder, IServiceProvider serviceProvider)
        {
            Field<PersonType>(
                "addPerson",
                description: "Adds new person.",
                arguments: new QueryArguments(
                    new QueryArgument<PersonInputType> { Name = "person" }
                ),
                resolve: context =>
                {
                    var person = context.GetArgument<Person>("person");

                    using (var connection = serviceProvider.GetRequiredService<IDbConnection>())
                    {
                        person.Id = person.MergedToPersonId = PostgreSql.NextIdentity(connection, (Person p) => p.Id);

                        bool success = SqlBuilder
                           .Insert(person)
                           .Execute(connection) > 0;

                        if (success)
                        {
                            var personMapper = new PersonEntityMapper();

                            var query = SqlBuilder
                                            .From<Person>("Person")
                                            .Select("FirstName, LastName")
                                            .Where("ID = @personId", new { personId = person.Id });

                            var results = query
                                   .Execute(connection, context.FieldAst, personMapper)
                                   .Distinct();
                            return results.FirstOrDefault();
                        }

                        return null;
                    }
                }
            );
        }
    }
}
