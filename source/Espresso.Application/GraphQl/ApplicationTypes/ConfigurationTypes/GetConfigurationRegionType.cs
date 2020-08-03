using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationRegionType : ObjectGraphType<GetConfigurationRegion>
    {
        public GetConfigurationRegionType()
        {
            Name = nameof(GetConfigurationRegion);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetConfigurationRegion.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationRegion.Name)
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<GetConfigurationNewsPortalType>>>>(
                name: nameof(GetConfigurationRegion.NewsPortals)
            );
        }
    }
}
