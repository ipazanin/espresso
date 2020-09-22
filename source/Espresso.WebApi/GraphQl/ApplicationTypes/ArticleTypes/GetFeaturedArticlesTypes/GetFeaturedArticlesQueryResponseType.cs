using Espresso.WebApi.Application.CQRS.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesQueryResponseType :
        ObjectGraphType<GetFeaturedArticlesQueryResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetFeaturedArticlesQueryResponseType()
        {
            Name = nameof(GetFeaturedArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetFeaturedArticlesArticleType>>>>(
                name: nameof(GetFeaturedArticlesQueryResponse.Articles)
            );
        }
    }
}
