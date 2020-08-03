using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationNewsPortalType : ObjectGraphType<GetConfigurationNewsPortal>
    {
        public GetConfigurationNewsPortalType()
        {
            Name = nameof(GetConfigurationNewsPortal);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetConfigurationNewsPortal.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationNewsPortal.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationNewsPortal.IconUrl)
            );
            Field<NonNullGraphType<BooleanGraphType>>(
                name: nameof(GetConfigurationNewsPortal.IsNew)
            );
            Field<IntGraphType>(
                name: nameof(GetConfigurationNewsPortal.RegionId)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetConfigurationNewsPortal.CategoryId)
            );
        }
    }
}
