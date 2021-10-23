// GetLatestArticlesNewsPortalType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetLatestArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetLatestArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLatestArticlesNewsPortalType : ObjectGraphType<GetLatestArticlesNewsPortal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetLatestArticlesNewsPortalType"/> class.
        /// </summary>
        public GetLatestArticlesNewsPortalType()
        {
            Name = nameof(GetLatestArticlesNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Id));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.Name));
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetLatestArticlesNewsPortal.IconUrl));
        }
    }
}
