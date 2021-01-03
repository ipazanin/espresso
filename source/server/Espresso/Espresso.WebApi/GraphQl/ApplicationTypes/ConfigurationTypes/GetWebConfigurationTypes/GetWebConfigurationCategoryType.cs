using Espresso.WebApi.Application.Configuration.Queries.GetWebConfiguration;
using GraphQL.Types;

namespace Espresso.WebApi.GraphQl.ApplicationTypes.ConfigurationTypes.GetWebCategoryArticlesTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWebConfigurationCategoryType : ObjectGraphType<GetWebConfigurationCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        public GetWebConfigurationCategoryType()
        {
            Name = nameof(GetWebConfigurationCategory);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(GetWebConfigurationCategory.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetWebConfigurationCategory.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetWebConfigurationCategory.Color)
            );
            Field<NonNullGraphType<IntGraphType>>(
                name: nameof(GetWebConfigurationCategory.CategoryType)
            );
            Field<IntGraphType>(
                name: nameof(GetWebConfigurationCategory.Position)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(GetWebConfigurationCategory.Url)
            );
        }
    }
}
