using Espresso.WebApi.GraphQl.ApplicationQueries;
using GraphQL;
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
        /// <param name="resolver"></param>
        /// <returns></returns>
        public GraphQlSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootGraphQlQuery>();
        }
    }
}
