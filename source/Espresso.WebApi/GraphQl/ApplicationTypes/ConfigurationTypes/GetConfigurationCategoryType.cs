using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetConfigurationCategoryType : ObjectGraphType<GetConfigurationCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetConfigurationCategoryType()
        {
            Name = nameof(GetConfigurationCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetConfigurationCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetConfigurationCategory.Color)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetConfigurationCategory.CategoryType)
            );
            Field<IntGraphType>(
                name: nameof(GetConfigurationCategory.Position)
            );
            Field<IntGraphType>(
                name: nameof(GetConfigurationCategory.Url)
            );
        }
    }
}
