using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using Espresso.Common.Enums;
using Espresso.Domain.Enums.ApplicationDownloadEnums;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes;
using GraphQL.Types;
using MediatR;

namespace Espresso.WebApi.GraphQl.ApplicationQueries.ConfigurationQueries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public GetConfigurationGraphQlQuery(IMediator mediator)
        {
            Name = "Configuration";
            FieldAsync<GetConfigurationQueryResponseType>(
                name: nameof(GetConfigurationGraphQlQuery),
                arguments: null,
                resolve: async resolveContext =>
                {
                    return await mediator.Send(
                        request: new GetConfigurationQuery(
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
