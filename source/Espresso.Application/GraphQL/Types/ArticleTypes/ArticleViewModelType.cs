using Espresso.Application.ViewModels.ArticleViewModels;
using GraphQL.Types;

namespace Espresso.Application.GraphQL.Types.ArticleTypes
{
    public class ArticleViewModelType : ObjectGraphType<ArticleViewModel>
    {
        public ArticleViewModelType()
        {
            Field(
                expression: articleViewModel => articleViewModel.Id,
                nullable: false,
                type: typeof(GuidGraphType)
            )
            .Description("Article Id");
        }
    }
}
