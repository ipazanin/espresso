using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes;
using Espresso.WebApi.GraphQl.Infrastructure;
using FluentValidation;
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
            Name = nameof(GetConfigurationGraphQlQuery);
            FieldAsync<GetConfigurationQueryResponseType>(
                name: nameof(GetConfigurationGraphQlQuery),
                arguments: null,
                resolve: async resolveContext =>
                {
                    return resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null ?
                        await mediator.Send(
                            request: new GetConfigurationQuery(
                                currentEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                targetedEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                consumerVersion: graphQlUserContext.ConsumerVersion,
                                deviceType: graphQlUserContext.DeviceType
                            ),
                            cancellationToken: resolveContext.CancellationToken
                        ) :
                        throw new ValidationException("Appropriate Headers must be defined!");
                },
                deprecationReason: null
            );
        }
    }
}
