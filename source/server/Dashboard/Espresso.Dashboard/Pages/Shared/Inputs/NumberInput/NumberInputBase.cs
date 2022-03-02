// NumberInputBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Shared.Inputs.NumberInput;

public class NumberInputBase : ComponentBase
{
    private readonly string _uniqueId = Guid.NewGuid().ToString();

    [Parameter]
    [EditorRequired]
    public string Headline { get; set; } = string.Empty;

    [Parameter]
    [EditorRequired]
    public int MaxValue { get; set; }

    [Parameter]
    [EditorRequired]
    public int MinValue { get; set; }

    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool isValid, string uniqueId)> IsValidChanged { get; set; }

    protected string ValidationClass { get; set; } = string.Empty;

    protected string ValidationMessage { get; set; } = string.Empty;

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
        if (!int.TryParse(args.Value?.ToString(), out var value))
        {
            _ = IsValidChanged.InvokeAsync((false, _uniqueId));
            return;
        }

        if (Value == value)
        {
            return;
        }

        Value = value;
        _ = ValueChanged.InvokeAsync(value);

        if (value > MaxValue)
        {
            ValidationClass = "is-invalid";
            ValidationMessage = $"Value must be less or equal to {MaxValue}";
            _ = IsValidChanged.InvokeAsync((false, _uniqueId));
        }
        else if (value < MinValue)
        {
            ValidationClass = "is-invalid";
            ValidationMessage = $"Value must be more or equal to {MinValue}";
            _ = IsValidChanged.InvokeAsync((false, _uniqueId));
        }
        else
        {
            ValidationClass = "is-valid";
            ValidationMessage = string.Empty;
            _ = IsValidChanged.InvokeAsync((true, _uniqueId));
        }
    }
}
