using System.Collections.Generic;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationMutations
{
    /// <summary>
    /// 
    /// </summary>
    public class RootGraphQlMutation : ObjectGraphType
    {
        /// <summary>
        /// 
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
