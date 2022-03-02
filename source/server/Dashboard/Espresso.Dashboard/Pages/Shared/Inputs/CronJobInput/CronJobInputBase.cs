// CronJobInputBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Cronos;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Shared.Inputs.CronJobInput;

public class CronJobInputBase : ComponentBase
{
    private readonly string _uniqueId = Guid.NewGuid().ToString();

    [Parameter]
    [EditorRequired]
    public string Headline { get; set; } = string.Empty;

    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool isValid, string uniqueId)> IsValidChanged { get; set; }

    protected string ValidationClass { get; set; } = string.Empty;

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        ValidationClass = string.Empty;
        StateHasChanged();
    }

    protected void OnInputValueChanged(ChangeEventArgs args)
    {
        var value = args.Value?.ToString() ?? string.Empty;

        if (Value == value)
        {
            return;
        }

        Value = value;
        _ = ValueChanged.InvokeAsync(value);

        try
        {
            _ = CronExpression.Parse(value);
            ValidationClass = "is-valid";
            _ = IsValidChanged.InvokeAsync((true, _uniqueId));
        }
        catch
        {
            ValidationClass = "is-invalid";
            _ = IsValidChanged.InvokeAsync((false, _uniqueId));
        }
    }
}
