// ExportImportDatabaseBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.Settings.ImportDatabase;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.Dashboard.Pages.DatabasePages;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class ExportImportDatabaseBase : ComponentBase
{
    private const int MaxAllowedFileSize = 8_000_000;

    protected string Errors { get; set; } = string.Empty;

    protected bool IsImportInProgress { get; private set; }

    [Inject]
    private IServiceScopeFactory ServiceScopeFactory { get; init; } = null!;

    [Inject]
    private IJsonService JsonService { get; init; } = null!;

    protected async Task OnImportDatabase(IBrowserFile databaseFile)
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        Errors = string.Empty;

        try
        {
            var fileStream = databaseFile.OpenReadStream(maxAllowedSize: MaxAllowedFileSize);
            var importDatabaseRequest = await JsonService.Deserialize<ImportDatabaseCommand>(fileStream, default);

            if (importDatabaseRequest is null)
            {
                return;
            }

            IsImportInProgress = true;
            StateHasChanged();
            _ = await sender.Send(importDatabaseRequest);
        }
        catch (Exception exception)
        {
            Errors = $"{exception.Message} - {exception.InnerException?.Message}";
        }
        finally
        {
            IsImportInProgress = false;
            StateHasChanged();
        }
    }
}
