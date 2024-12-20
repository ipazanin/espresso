﻿// RssFeedDetailsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Application.DataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Espresso.Dashboard.Application.RssFeeds.Commands.UpdateRssFeed;
using Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;
using Espresso.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;

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

    protected IEnumerable<ParsingErrorMessageDto> ParsingMessages { get; private set; } = [];

    protected string ParsingMessagesSearchString { get; set; } = string.Empty;

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

    protected static Color GetColorFromLogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Error or LogLevel.Critical => Color.Error,
            LogLevel.Warning => Color.Warning,
            LogLevel.Trace or
            LogLevel.Debug or
            LogLevel.Information or
            LogLevel.None or
            _ => Color.Info,
        };
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

        ParsingMessages = [.. ParsingMessagesService
            .GetMessages(Id)
            .OrderByDescending(message => message.Created)];

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
        await sender.Send(command);

        NavigationManager.NavigateTo("rss-feeds");
    }

    protected bool FilterParsingMessages(ParsingErrorMessageDto parsingMessage)
    {
        if (string.IsNullOrWhiteSpace(ParsingMessagesSearchString))
        {
            return true;
        }

        return parsingMessage.LogLevel.ToString().Contains(ParsingMessagesSearchString, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.Message.Contains(ParsingMessagesSearchString, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.RssFeedId.ToString(CultureInfo.InvariantCulture).Contains(ParsingMessagesSearchString, StringComparison.OrdinalIgnoreCase);
    }

    private async Task RefreshParsedMessages()
    {
        while (!_disposedValue)
        {
            await Task.Delay(1000);

            ParsingMessages = [.. ParsingMessagesService
                .GetMessages(Id)
                .OrderByDescending(message => message.Created)];

            await InvokeAsync(StateHasChanged);
        }
    }
}
