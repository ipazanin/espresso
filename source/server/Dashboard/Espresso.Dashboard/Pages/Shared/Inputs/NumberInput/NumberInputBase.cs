// NumberInputBase.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Shared.Inputs.NumberInput;

public class NumberInputBase : ComponentBase
{
    private int _value;

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
    public int Value
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            _ = ValueChanged.InvokeAsync(value);

            if (value > MaxValue)
            {
                ValidationClass = "is-invalid";
                ValidationMessage = $"Value must be less or equal to {MaxValue}";
            }
            else if (value < MinValue)
            {
                ValidationClass = "is-invalid";
                ValidationMessage = $"Value must be more or equal to {MinValue}";
            }
            else
            {
                ValidationClass = "is-valid";
                ValidationMessage = string.Empty;
            }
        }
    }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

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
}
