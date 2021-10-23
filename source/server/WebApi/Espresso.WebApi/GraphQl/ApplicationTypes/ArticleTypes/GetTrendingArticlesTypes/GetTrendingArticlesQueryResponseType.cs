// GetTrendingArticlesQueryResponseType.cs
//
// © 2021 Espresso News. All rights reserved.

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
        /// Initializes a new instance of the <see cref="GetTrendingArticlesQueryResponseType"/> class.
        /// </summary>
        public GetTrendingArticlesQueryResponseType()
        {
            Name = nameof(GetTrendingArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetTrendingArticlesArticleType>>>>(
                name: nameof(GetTrendingArticlesQueryResponse.Articles));
        }
    }
}
