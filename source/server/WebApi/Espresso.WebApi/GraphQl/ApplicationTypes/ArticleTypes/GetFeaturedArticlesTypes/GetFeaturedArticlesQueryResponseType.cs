// GetFeaturedArticlesQueryResponseType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
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
        /// Initializes a new instance of the <see cref="GetFeaturedArticlesQueryResponseType"/> class.
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
