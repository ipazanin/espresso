// GraphQlSchema.cs
//
// © 2021 Espresso News. All rights reserved.

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
        /// Initializes a new instance of the <see cref="GraphQlSchema"/> class.
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
