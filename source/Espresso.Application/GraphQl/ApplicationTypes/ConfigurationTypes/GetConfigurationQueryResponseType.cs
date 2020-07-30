using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationQueryResponseType :
        ObjectGraphType<GetConfigurationQueryResponse>
    {
        public GetConfigurationQueryResponseType()
        {
            Name = nameof(GetConfigurationQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CategoryViewModelType>>>>(
                name: nameof(GetConfigurationQueryResponse.Categories)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<CategoryViewModelWithNewsPortalsType>>>>(
                name: nameof(GetConfigurationQueryResponse.CategoryViewModelWithNewsPortals)
            );
        }
    }
}
