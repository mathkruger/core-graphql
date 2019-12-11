using GraphQL.Types;
using grphql_test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.GraphQL
{
    public class EmailType :
        ObjectGraphType<Email>
    {
        public EmailType()
        {
            Name = "email";
            Description = "An email address.";

            Field<IntGraphType>(
                "id",
                description: "A unique identifier for the email address.",
                resolve: context => context.Source?.Id
            );

            Field<StringGraphType>(
                "address",
                description: "The email address.",
                resolve: context => context.Source?.Address
            );
        }
    }
}
