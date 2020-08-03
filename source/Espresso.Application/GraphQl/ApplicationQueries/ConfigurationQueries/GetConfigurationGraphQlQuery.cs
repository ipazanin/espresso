using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes;
using Espresso.Application.GraphQl.Infrastructure;
using FluentValidation;
using GraphQL.Types;
using MediatR;

namespace Espresso.Application.GraphQl.ApplicationQueries.ConfigurationQueries
{
    public class GetConfigurationGraphQlQuery : ObjectGraphType, IGraphQlQuery
    {
        public GetConfigurationGraphQlQuery(IMediator mediator)
        {
            Name = nameof(GetConfigurationGraphQlQuery);
            FieldAsync<GetConfigurationQueryResponseType>(
                name: nameof(GetConfigurationGraphQlQuery),
                arguments: null,
                resolve: async resolveContext =>
                {
                    if (
                        resolveContext.UserContext is GraphQlApplicationContext graphQlUserContext &&
                        graphQlUserContext != null
                    )
                    {
                        return await mediator.Send(
                            request: new GetConfigurationQuery(
                                currentEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                targetedEspressoWebApiVersion: graphQlUserContext.CurrentEspressoWebApiVersion,
                                consumerVersion: graphQlUserContext.ConsumerVersion,
                                deviceType: graphQlUserContext.DeviceType
                            ),
                            cancellationToken: resolveContext.CancellationToken
                        );
                    }
                    else
                    {
                        throw new ValidationException("Appropriate Headers must be defined!");
                    }
                },
                deprecationReason: null
            );
        }
    }
}
