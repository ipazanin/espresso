// GetCategoryArticlesArticleType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesArticleType : ObjectGraphType<GetCategoryArticlesArticle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesArticleType"/> class.
        /// </summary>
        public GetCategoryArticlesArticleType()
        {
            Name = nameof(GetCategoryArticlesArticle);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Title));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.Url));
            Field<StringGraphType>(
                name: nameof(GetCategoryArticlesArticle.ImageUrl));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesArticle.PublishDateTime));
            Field<NonNullGraphType<GetCategoryArticlesNewsPortalType>>(
                name: nameof(GetCategoryArticlesArticle.NewsPortal));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetCategoryArticlesCategoryType>>>>(
                name: nameof(GetCategoryArticlesArticle.Categories));
        }
    }
}
