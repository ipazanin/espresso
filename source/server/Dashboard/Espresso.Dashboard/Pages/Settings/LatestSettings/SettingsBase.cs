// SettingsBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Cronos;
using Espresso.Dashboard.Application.Settings.Commands.UpdateSetting;
using Espresso.Dashboard.Application.Settings.Queries.GetLatestSetting;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Espresso.Dashboard.Pages.Settings.LatestSettings;

public class SettingsBase : ComponentBase
{
    [Inject]
    protected IServiceScopeFactory ServiceScopeFactory { get; init; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; init; } = null!;

    protected GetLatestSettingQueryResponse? GetLatestSettingQueryResponse { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    protected string[] Errors { get; set; } = [];
#pragma warning restore CA1819 // Properties should not return arrays

    protected MudForm? Form { get; set; }

    protected bool Success { get; set; }

    protected Func<string?, string?> ValidateCronExpression { get; } = new Func<string?, string?>((inputValue) =>
    {
        try
        {
            _ = CronExpression.Parse(inputValue);
            return null;
        }
        catch
        {
            return "Invalid Cron Expression! Check https://crontab.guru/ for more info on CRON expressions.";
        }
    });

    protected static string GetCronExpressionNextOccurrence(string inputValue)
    {
        try
        {
            var cronExpression = CronExpression.Parse(inputValue);
            var nextOccurrence = cronExpression.GetNextOccurrence(DateTime.UtcNow);

            return $"Next occurrence: {nextOccurrence}";
        }
        catch
        {
            return string.Empty;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        GetLatestSettingQueryResponse = await sender.Send(new GetLatestSettingQuery());
    }

    protected async Task OnSaveButtonClick()
    {
        if (!Success)
        {
            return;
        }

        if (GetLatestSettingQueryResponse is null)
        {
            return;
        }

        using var scope = ServiceScopeFactory.CreateAsyncScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new UpdateSettingCommand(GetLatestSettingQueryResponse.Setting);
        await sender.Send(command);

        NavigationManager.NavigateTo("/");
    }
}
