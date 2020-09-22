using Espresso.WebApi.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetConfigurationTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationNewsPortalType : ObjectGraphType<GetConfigurationNewsPortal>
    {
        /// <summary>
        /// 
        /// </summary>
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
