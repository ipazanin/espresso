// NewSourcesRequestCommandHandler.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Services.Contracts;
using MediatR;

namespace Espresso.WebApi.Application.NewsPortals.Commands.NewSourcesRequest;

public class NewSourcesRequestCommandHandler : IRequestHandler<NewsSourcesRequestCommand>
{
    private readonly ISlackService _slackService;

    /// <summary>
    /// Initializes a new instance of the <see cref="NewSourcesRequestCommandHandler"/> class.
    /// </summary>
    /// <param name="slackService"></param>
    public NewSourcesRequestCommandHandler(
        ISlackService slackService)
    {
        _slackService = slackService;
    }

    public async Task<Unit> Handle(
        NewsSourcesRequestCommand request,
        CancellationToken cancellationToken)
    {
        await _slackService
            .LogNewNewsPortalRequest(
                newsPortalName: request.NewsPortalName,
                email: request.Email,
                url: request.Url,
                cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
