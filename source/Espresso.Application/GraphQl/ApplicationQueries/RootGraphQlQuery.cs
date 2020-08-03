using System.Collections.Generic;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationQueries
{
    public class RootGraphQlQuery : ObjectGraphType
    {
        public RootGraphQlQuery(IEnumerable<IGraphQlQuery> graphQlQueries)
        {
            Name = nameof(RootGraphQlQuery);
            foreach (var marker in graphQlQueries)
            {
                if (marker is ObjectGraphType<object> q)
                {
                    foreach (var f in q.Fields)
                    {
                        AddField(f);
                    }
                }
            }
        }
    }
}
