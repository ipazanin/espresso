using Espresso.Application.ViewModels.CategoryViewModels;
using Espresso.Application.ViewModels.NewsPortalViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQl.ApplicationTypes.ArticleTypes
{
    public class CategoryViewModelType : ObjectGraphType<CategoryViewModel>
    {
        public CategoryViewModelType()
        {
            Name = nameof(NewsPortalViewModelType);
            Field<NonNullGraphType<IdGraphType>>(
                name: nameof(CategoryViewModel.Id)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(CategoryViewModel.Name)
            );
            Field<NonNullGraphType<StringGraphType>>(
                name: nameof(CategoryViewModel.Color)
            );
        }
    }
}
