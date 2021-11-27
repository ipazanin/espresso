// NewSourcesRequestCommandValidator.cs
//
// © 2021 Espresso News. All rights reserved.

using FluentValidation;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest;

public class NewSourcesRequestCommandValidator : AbstractValidator<NewsSourcesRequestCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewSourcesRequestCommandValidator"/> class.
    /// </summary>
    public NewSourcesRequestCommandValidator()
    {
        RuleFor(request => request.NewsPortalName).NotEmpty();
        RuleFor(request => request.Email).NotEmpty().EmailAddress();
    }
}
