using System;
using Espresso.WebApi.GraphQl.ApplicationMutations;
using Espresso.WebApi.GraphQl.ApplicationQueries;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationSchema
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphQlSchema : Schema, ISchema
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public GraphQlSchema(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = serviceProvider.GetService(typeof(RootGraphQlQuery)) as RootGraphQlQuery;
            Mutation = serviceProvider.GetService(typeof(RootGraphQlMutation)) as RootGraphQlMutation;
        }
    }
}
