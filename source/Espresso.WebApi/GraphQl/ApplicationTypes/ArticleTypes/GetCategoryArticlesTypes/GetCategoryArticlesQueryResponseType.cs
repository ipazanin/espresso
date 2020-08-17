﻿using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesQueryResponseType :
        ObjectGraphType<GetCategoryArticlesQueryResponse>
    {
        /// <summary>
        /// 
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