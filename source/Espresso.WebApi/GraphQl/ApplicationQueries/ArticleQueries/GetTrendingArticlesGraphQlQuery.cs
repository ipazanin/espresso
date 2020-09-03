using Espresso.Application.CQRS.Articles.Queries.GetTrendingArticles;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ArticleTypes.GetTrendingArticlesTypes;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ArticlesQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrendingArticlesGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetTrendingArticlesGraphQlQuery(IMediator mediator, IWebApiConfiguration webApiConfiguration)
        {
            FieldAsync<GetTrendingArticlesQueryResponseType>(
                name: "trendingArticles",
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
                        }
                    }
                ),
                resolve: async resolveContext =>
                {
                    return await mediator.Send(
                        request: new GetTrendingArticlesQuery(
                            take: (int)resolveContext.Arguments["take"],
                            skip: (int)resolveContext.Arguments["take"],
                            maxAgeOfTrendingArticle: webApiConfiguration.AppConfiguration.MaxAgeOfTrendingArticle,
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
