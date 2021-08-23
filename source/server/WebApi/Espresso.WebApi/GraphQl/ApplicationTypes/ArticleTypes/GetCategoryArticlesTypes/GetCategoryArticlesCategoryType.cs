// GetCategoryArticlesCategoryType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesCategoryType : ObjectGraphType<GetCategoryArticlesCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesCategoryType"/> class.
        /// </summary>
        public GetCategoryArticlesCategoryType()
        {
            Name = nameof(GetCategoryArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesCategory.Color)
            );
        }
    }
}
