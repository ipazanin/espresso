// NumberInputBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Shared.Inputs.NumberInput;

public class NumberInputBase : InputBase<int>
{
    [Parameter]
    [EditorRequired]
    public int MaxValue { get; set; }

    [Parameter]
    [EditorRequired]
    public int MinValue { get; set; }

    protected string ValidationMessage { get; set; } = string.Empty;

    protected void OnInputValueChanged(ChangeEventArgs args)
    {
        if (!int.TryParse(args.Value?.ToString(), out var value))
        {
            _ = IsValidChanged.InvokeAsync((false, UniqueId));
            ValidationClass = "is-invalid";
            ValidationMessage = "Value is not integer";
            return;
        }

        Value = value;
        _ = ValueChanged.InvokeAsync(value);

        if (value > MaxValue)
        {
            ValidationClass = "is-invalid";
            ValidationMessage = $"Value must be less or equal to {MaxValue}";
            _ = IsValidChanged.InvokeAsync((false, UniqueId));
        }
        else if (value < MinValue)
        {
            ValidationClass = "is-invalid";
            ValidationMessage = $"Value must be more or equal to {MinValue}";
            _ = IsValidChanged.InvokeAsync((false, UniqueId));
        }
        else
        {
            ValidationClass = "is-valid";
            ValidationMessage = string.Empty;
            _ = IsValidChanged.InvokeAsync((true, UniqueId));
        }
    }
}
