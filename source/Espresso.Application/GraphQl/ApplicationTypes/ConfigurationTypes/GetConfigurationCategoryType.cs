using Espresso.Application.CQRS.Articles.Queries.GetCategoryArticles;
using Espresso.Application.CQRS.Configuration.Queries.GetConfiguration;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ConfigurationTypes
{
    public class GetConfigurationCategoryType : ObjectGraphType<GetConfigurationCategory>
    {
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
        }
    }
}
