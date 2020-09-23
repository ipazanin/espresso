using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesQueryResponseType :
        ObjectGraphType<GetTrendingArticlesQueryResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTrendingArticlesQueryResponseType()
        {
            Name = nameof(GetTrendingArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetTrendingArticlesArticleType>>>>(
                name: nameof(GetTrendingArticlesQueryResponse.Articles)
            );
        }
    }
}
