﻿// GetCategoryArticlesQueryResponseType.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.ConfigurationTypes.GetWebCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesQueryResponseType :
        ObjectGraphType<GetCategoryArticlesQueryResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryArticlesQueryResponseType"/> class.
        /// </summary>
        public GetCategoryArticlesQueryResponseType()
        {
            Name = nameof(GetCategoryArticlesQueryResponse);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetCategoryArticlesArticleType>>>>(
                name: nameof(GetCategoryArticlesQueryResponse.Articles)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetCategoryArticlesQueryResponse.NewNewsPortalsPosition)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetCategoryArticlesNewsPortalType>>>>(
                name: nameof(GetCategoryArticlesQueryResponse.NewNewsPortals)
            );
        }
    }
}