using GraphQL.Types;
using grphql_test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.GraphQL
{
    public class CompanyType :
        ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Name = "company";
            Description = "A company.";

            Field<IntGraphType>(
                "id",
                description: "A unique identifier for the company.",
                resolve: context => context.Source?.Id
            );

            Field<StringGraphType>(
                "name",
                description: "The name of the company.",
                resolve: context => context.Source?.Name
            );

            Field<ListGraphType<EmailType>>(
                "emails",
                description: "A list of email addresses for the company.",
                resolve: context => context.Source?.Emails
            );

            Field<ListGraphType<PhoneType>>(
                "phones",
                description: "A list of phone numbers for the company.",
                resolve: context => context.Source?.Phones
            );
        }
    }
}
