// RootGraphQlMutation.cs
//
// © 2021 Espresso News. All rights reserved.

using GraphQL.Types;
using System.Collections.Generic;

namespace Espresso.WebApi.GraphQl.ApplicationMutations
{
    /// <summary>
    /// 
    /// </summary>
    public class RootGraphQlMutation : ObjectGraphType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootGraphQlMutation"/> class.
        /// </summary>
        /// <param name="graphQlMutations"></param>
        public RootGraphQlMutation(IEnumerable<IGraphQlMutation> graphQlMutations)
        {
            foreach (var marker in graphQlMutations)
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
