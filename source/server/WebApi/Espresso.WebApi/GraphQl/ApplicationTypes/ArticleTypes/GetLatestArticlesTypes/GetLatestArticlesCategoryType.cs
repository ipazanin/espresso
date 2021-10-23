// GetLatestArticlesCategoryType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesCategoryType : ObjectGraphType<GetLatestArticlesCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesCategoryType"/> class.
        /// </summary>
        public GetLatestArticlesCategoryType()
        {
            Name = nameof(GetLatestArticlesCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesCategory.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Name));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesCategory.Color));
        }
    }
}
