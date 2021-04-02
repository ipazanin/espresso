﻿using System;
using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetWebCategoryArticlesTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ConfigurationQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWebConfigurationGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetWebConfigurationGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<GetWebConfigurationQueryResponseType>(
                name: "webConfiguration",
                arguments: null,
                resolve: async resolveContext =>
                {
                    var userContext = resolveContext.UserContext as GraphQlUserContext ??
                        throw new Exception("Invalid GraphQL User Context");

                    return await mediator.Send(
                        request: new GetWebConfigurationQuery
                        {
                            MaxAgeOfNewNewsPortal = webApiConfiguration.DateTimeConfiguration.MaxAgeOfNewNewsPortal,
                            TargetedApiVersion = userContext.TargetedApiVersion,
                            ConsumerVersion = userContext.ConsumerVersion,
                            DeviceType = userContext.DeviceType,
                        },
                        cancellationToken: resolveContext.CancellationToken
                    );
                },
                deprecationReason: null
            );
        }
    }
}