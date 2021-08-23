// GetCategoryArticlesNewsPortalType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesNewsPortalType : ObjectGraphType<GetCategoryArticlesNewsPortal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesNewsPortalType"/> class.
        /// </summary>
        public GetCategoryArticlesNewsPortalType()
        {
            Name = nameof(GetCategoryArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetCategoryArticlesNewsPortal.IconUrl)
            );
        }
    }
}
