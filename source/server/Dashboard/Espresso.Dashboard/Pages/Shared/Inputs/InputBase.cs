// InputBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Shared.Inputs;

public class InputBase<TValue> : ComponentBase
    where TValue : notnull
{
    [Parameter]
    [EditorRequired]
    public string Headline { get; set; } = string.Empty;

    [Parameter]
    [EditorRequired]
    public EventCallback<(bool isValid, string uniqueId)> IsValidChanged { get; set; }

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected string UniqueId { get; } = Guid.NewGuid().ToString();

    protected string ValidationClass { get; set; } = string.Empty;
}
