// GetTrendingArticlesCategoryType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetTrendingArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesCategoryType : ObjectGraphType<GetTrendingArticlesCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTrendingArticlesCategoryType"/> class.
        /// </summary>
        public GetTrendingArticlesCategoryType()
        {
            Name = nameof(GetTrendingArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetTrendingArticlesCategory.Color)
            );
        }
    }
}
