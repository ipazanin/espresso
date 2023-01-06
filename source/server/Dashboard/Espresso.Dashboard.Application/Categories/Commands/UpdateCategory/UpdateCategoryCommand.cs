// UpdateCategoryCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CategoryDataTransferObjects;
using MediatR;

namespace Espresso.Dashboard.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public UpdateCategoryCommand(CategoryDto category)
    {
        Category = category;
    }

    public CategoryDto Category { get; }
}
