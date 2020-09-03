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
        /// <param name="configuration"></param>
        public GetCategoryArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration configuration)
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
                            take: (int?)resolveContext.Arguments["take"],
                            skip: (int?)resolveContext.Arguments["skip"],
                            newsPortalIdsString: (string?)resolveContext.Arguments["newsPortalIds"],
                            categoryId: (int)resolveContext.Arguments["categoryId"],
                            regionId: (int?)resolveContext.Arguments["regionId"],
                            newNewsPortalsPosition: configuration.AppConfiguration.NewNewsPortalsPosition,
                            titleSearchQuery: (string?)resolveContext.Arguments["titleSearchQuery"],
                            currentEspressoWebApiVersion: (string)resolveContext.UserContext["currentEspressoWebApiVersion"],
                            targetedEspressoWebApiVersion: (string)resolveContext.UserContext["targetedEspressoWebApiVersion"],
                            consumerVersion: (string)resolveContext.UserContext["consumerVersion"],
                            deviceType: (DeviceType)resolveContext.UserContext["deviceType"],
                            appEnvironment: (AppEnvironment)resolveContext.UserContext["appEnvironment"]
                        ),
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}
