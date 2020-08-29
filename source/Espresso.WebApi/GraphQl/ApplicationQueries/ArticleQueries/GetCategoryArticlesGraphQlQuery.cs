using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using Espresso.Common.Constants;
using GraphQL.Types;
using MediatR;
using Espresso.WebApi.Configuration;
using Espresso.Domain.Enums.ApplicationDownloadEnums;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCategoryArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="configuration"></param>
        public GetCategoryArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration configuration)
        {
            Name = "CategoryArticles";
            FieldAsync<GetCategoryArticlesQueryResponseType>(
                name: nameof(GetCategoryArticlesGraphQlQuery),
                arguments: new QueryArguments(
                    args: new QueryArgument[]
                    {
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "take",
                            DefaultValue = DefaultValueConstants.DefaultTake
                        },
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "skip",
                            DefaultValue = DefaultValueConstants.DefaultSkip
                        },
                        new QueryArgument<StringGraphType>
                        {
                            Name = "newsPortalIds",
                        },
                        new QueryArgument<NonNullGraphType<IntGraphType>>
                        {
                            Name = "categoryId",
                        },
                        new QueryArgument<IntGraphType>
                        {
                            Name = "regionId",
                        },
                        new QueryArgument<IntGraphType>
                        {
                            Name = "titleSearchQuery",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    return await mediator.Send(
                        request: new GetCategoryArticlesQuery(
                            take: (int?)resolveContext.Arguments["take"],
                            skip: (int?)resolveContext.Arguments["skip"],
                            newsPortalIdsString: (string?)resolveContext.Arguments["newsPortalIds"],
                            categoryId: (int)resolveContext.Arguments["categoryId"],
                            regionId: (int?)resolveContext.Arguments["regionId"],
                            newNewsPortalsPosition: configuration.NewNewsPortalsPosition,
                            titleSearchQuery: (string?)resolveContext.Arguments["titleSearchQuery"],
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"]
                        ),
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
