using Dapper.GraphQL;
using GraphQL.Language.AST;
using grphql_test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.QueryBuilders
{
    public class EmailQueryBuilder :
        IQueryBuilder<Email>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            // Always get the ID of the email
            query.Select($"{alias}.Id");
            // Tell Dapper where the Email class begins (at the Id we just selected)
            query.SplitOn<Email>("Id");

            var fields = context.GetSelectedFields();
            if (fields.ContainsKey("address"))
            {
                query.Select($"{alias}.Address");
            }

            return query;
        }
    }
}
