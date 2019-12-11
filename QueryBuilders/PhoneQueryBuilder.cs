using Dapper.GraphQL;
using GraphQL.Language.AST;
using grphql_test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.QueryBuilders
{
    public class PhoneQueryBuilder :
        IQueryBuilder<Phone>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            query.Select($"{alias}.Id");
            query.SplitOn<Phone>("Id");

            var fields = context.GetSelectedFields();
            foreach (var kvp in fields)
            {
                switch (kvp.Key)
                {
                    case "number": query.Select($"{alias}.Number"); break;
                    case "type": query.Select($"{alias}.Type"); break;
                }
            }

            return query;
        }
    }
}
