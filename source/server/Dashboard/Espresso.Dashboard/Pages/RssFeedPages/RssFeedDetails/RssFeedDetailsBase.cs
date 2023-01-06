// RssFeedDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.RssFeeds.Commands.UpdateRssFeed;
using Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.RssFeedPages.RssFeedDetails;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class RssFeedDetailsBase : ComponentBase, IDisposable
{
    private bool _disposedValue;

    /// <summary>
    /// Gets <see cref="NewsPortal"/> id.
    /// </summary>
    [Parameter]
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets <see cref="NewsPortal"/> details.
    /// </summary>
    protected GetRssFeedDetailsQueryResponse? RssFeedDetails { get; set; }

    protected bool Success { get; set; }

    protected IEnumerable<ParsingErrorMessageDto> ParsingMessages { get; private set; } = Enumerable.Empty<ParsingErrorMessageDto>();

    /// <summary>
    /// Gets <see cref="Mediator"/> request sender.
    /// </summary>
    [Inject]
    private IServiceScopeFactory ServiceScopeFactory { get; init; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; init; } = null!;

    [Inject]
    private IParsingMessagesService ParsingMessagesService { get; init; } = null!;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            _disposedValue = true;
        }
    }

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        RssFeedDetails = await sender.Send(new GetRssFeedDetailsQuery(Id));

        ParsingMessages = ParsingMessagesService.GetMessages(Id).ToArray();

        _ = Task.Run(RefreshParsedMessages);
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (RssFeedDetails is null)
        {
            return;
        }

        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateRssFeedCommand(
            rssFeed: RssFeedDetails.RssFeed,
            rssFeedCategories: RssFeedDetails.RssFeedCategories,
            rssFeedContentModifiers: RssFeedDetails.RssFeedContentModifiers);
        _ = await sender.Send(command);

        NavigationManager.NavigateTo("rss-feeds");
    }

    private async Task RefreshParsedMessages()
    {
        while (!_disposedValue)
        {
            await Task.Delay(1000);

            ParsingMessages = ParsingMessagesService.GetMessages(Id).ToArray();

            await InvokeAsync(StateHasChanged);
        }
    }
}
