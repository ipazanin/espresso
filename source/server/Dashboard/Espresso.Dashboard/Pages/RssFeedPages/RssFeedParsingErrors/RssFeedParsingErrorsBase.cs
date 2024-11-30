// RssFeedParsingErrorsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Globalization;
using Espresso.Application.DataTransferObjects;
using Espresso.Dashboard.Application.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Espresso.Dashboard.Pages.RssFeedPages.RssFeedParsingErrors;

public class RssFeedParsingErrorsBase : ComponentBase, IDisposable
{
    private bool _disposedValue;

    protected IEnumerable<ParsingErrorMessageDto> ParsingMessages { get; private set; } = [];

    protected string SearchString { get; set; } = string.Empty;

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

    protected bool Filter(ParsingErrorMessageDto parsingMessage)
    {
        if (string.IsNullOrWhiteSpace(SearchString))
        {
            return true;
        }

        return parsingMessage.LogLevel.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.Message.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.RssFeedId.ToString(CultureInfo.InvariantCulture).Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        ParsingMessages = [.. ParsingMessagesService
            .GetMessages()
            .OrderByDescending(message => message.Created)];
    }

    protected void OnSearch(string searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
        {
            return;
        }

        ParsingMessages = ParsingMessages
            .Where(parsingMessage => parsingMessage.LogLevel.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.Message.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                parsingMessage.RssFeedId.ToString(CultureInfo.InvariantCulture).Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }
}
