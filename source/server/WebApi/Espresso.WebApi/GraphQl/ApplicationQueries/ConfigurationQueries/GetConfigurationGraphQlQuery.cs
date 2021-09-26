// GetConfigurationGraphQlQuery.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.WebApi.Application.Configuration.Queries.GetConfiguration;
using Espresso.WebApi.Configuration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetConfigurationTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using GraphQL.Types;
using MediatR;
using System;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ConfigurationQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetConfigurationGraphQlQuery"/> class.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="webApiConfiguration"></param>
        public GetConfigurationGraphQlQuery(
            IMediator mediator,
            IWebApiConfiguration webApiConfiguration
        )
        {
            FieldAsync<GetConfigurationQueryResponseType>(
                name: "configuration",
                arguments: null,
                resolve: async resolveContext =>
                {
                    var userContext = resolveContext.UserContext as GraphQlUserContext ??
                        throw new Exception("Invalid GraphQL User Context");

                    return await mediator.Send(
                        request: new GetConfigurationQuery
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
