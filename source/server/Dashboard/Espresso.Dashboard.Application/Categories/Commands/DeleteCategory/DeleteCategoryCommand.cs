// DeleteCategoryCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; }
}
