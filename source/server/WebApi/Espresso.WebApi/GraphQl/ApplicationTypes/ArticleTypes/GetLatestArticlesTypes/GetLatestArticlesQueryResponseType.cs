// GetLatestArticlesQueryResponseType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesQueryResponseType :
        ObjectGraphType<GetLatestArticlesQueryResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesQueryResponseType"/> class.
        /// </summary>
        public GetLatestArticlesQueryResponseType()
        {
            Name = nameof(GetLatestArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetLatestArticlesArticleType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.Articles)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortalsPosition)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetLatestArticlesNewsPortalType>>>>(
                name: nameof(GetLatestArticlesQueryResponse.NewNewsPortals)
            );
        }
    }
}
