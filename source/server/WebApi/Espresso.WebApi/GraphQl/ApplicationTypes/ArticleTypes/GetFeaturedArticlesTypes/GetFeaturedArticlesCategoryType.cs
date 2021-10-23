// GetFeaturedArticlesCategoryType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesCategoryType : ObjectGraphType<GetFeaturedArticlesCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeaturedArticlesCategoryType"/> class.
        /// </summary>
        public GetFeaturedArticlesCategoryType()
        {
            Name = nameof(GetFeaturedArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Name));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesCategory.Color));
        }
    }
}
