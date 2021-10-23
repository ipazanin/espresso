// GetTrendingArticlesNewsPortalType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesNewsPortalType : ObjectGraphType<GetTrendingArticlesNewsPortal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingArticlesNewsPortalType"/> class.
        /// </summary>
        public GetTrendingArticlesNewsPortalType()
        {
            Name = nameof(GetTrendingArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.Name));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesNewsPortal.IconUrl));
        }
    }
}
