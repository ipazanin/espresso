// RootGraphQlQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using GraphQL.Types;
using System.Collections.Generic;

namespace Espresso.WebApi.GraphQl.ApplicationQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class RootGraphQlQuery : ObjectGraphType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootGraphQlQuery"/> class.
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
