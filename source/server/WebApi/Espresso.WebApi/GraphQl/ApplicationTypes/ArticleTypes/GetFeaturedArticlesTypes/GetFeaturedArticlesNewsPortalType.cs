// GetFeaturedArticlesNewsPortalType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetFeaturedArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetFeaturedArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeaturedArticlesNewsPortalType : ObjectGraphType<GetFeaturedArticlesNewsPortal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFeaturedArticlesNewsPortalType"/> class.
        /// </summary>
        public GetFeaturedArticlesNewsPortalType()
        {
            Name = nameof(GetFeaturedArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetFeaturedArticlesNewsPortal.IconUrl)
            );
        }
    }
}
