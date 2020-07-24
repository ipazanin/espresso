using System.Threading.Tasks;
using Espresso.Application.GraphQL.Types.ArticleTypes;
using Espresso.Application.ViewModels.ArticleViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQL.Queries.ArticlesQueries
{
    public class GetLatestArticlesGraphQLQuery : ObjectGraphType
    {
        public GetLatestArticlesGraphQLQuery()
        {
            FieldAsync<ArticleViewModelType>(
                name: "",
                description: "",
                arguments: new QueryArguments(),
                resolve: context => Task.FromResult(new object { }),
                deprecationReason:
            null
            );
        }
    }
}
