using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.GraphQL
{
    public class PersonSchema :
        global::GraphQL.Types.Schema
    {
        public PersonSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<PersonQuery>();
            Mutation = resolver.Resolve<PersonMutation>();
        }
    }
}
