using Espresso.Application.GraphQl.ApplicationQueries;
using GraphQL;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationSchema
{
    public class GraphQlSchema : Schema, ISchema
    {
        public GraphQlSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootGraphQlQuery>();
        }
    }
}
