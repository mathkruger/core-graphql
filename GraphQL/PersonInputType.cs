using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.GraphQL
{
    public class PersonInputType : InputObjectGraphType
    {
        public PersonInputType()
        {
            Name = "PersonInput";
            Field<StringGraphType>("firstName");
            Field<StringGraphType>("lastName");
        }
    }
}
