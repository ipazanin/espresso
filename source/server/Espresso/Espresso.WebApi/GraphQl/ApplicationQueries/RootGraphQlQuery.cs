using System.Collections.Generic;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class RootGraphQlQuery : ObjectGraphType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphQlQueries"></param>
        public RootGraphQlQuery(IEnumerable<IGraphQlQuery> graphQlQueries)
        {
            Name = "RootQuery";
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
