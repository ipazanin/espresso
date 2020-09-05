using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetCategoryArticlesTypes;
using GraphQL.Types;
using MediatR;

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
        /// <param name="webApiConfiguration"></param>
        public GetCategoryArticlesGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<GetCategoryArticlesQueryResponseType>(
                name: "categoryArticles",
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
                        new QueryArgument<LongGraphType>
                        {
                            Name = "minTimestamp",
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
                        new QueryArgument<StringGraphType>
                        {
                            Name = "titleSearchQuery",
                        },
                    }
                ),
                resolve: async resolveContext =>
                {
                    return await mediator.Send(
                        request: new GetCategoryArticlesQuery(
                            take: (int)resolveContext.Arguments["take"],
                            skip: (int)resolveContext.Arguments["skip"],
                            minTimestamp: (long?)resolveContext.Arguments["minTimestamp"],
                            newsPortalIdsString: (string?)resolveContext.Arguments["newsPortalIds"],
                            categoryId: (int)resolveContext.Arguments["categoryId"],
                            regionId: (int?)resolveContext.Arguments["regionId"],
                            newNewsPortalsPosition: webApiConfiguration.AppConfiguration.NewNewsPortalsPosition,
                            titleSearchQuery: (string?)resolveContext.Arguments["titleSearchQuery"],
                            maxAgeOfNewNewsPortal: webApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"],
                            appEnvironment: webApiConfiguration.AppConfiguration.AppEnvironment
                        ),
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
