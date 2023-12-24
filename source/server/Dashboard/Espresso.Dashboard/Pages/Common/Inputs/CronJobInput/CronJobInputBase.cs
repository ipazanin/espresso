// CronJobInputBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Cronos;
using Microsoft.AspNetCore.Components;

namespace Espresso.Dashboard.Pages.Common.Inputs.CronJobInput;

public class CronJobInputBase : InputBase<string>
{
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
            _ = IsValidChanged.InvokeAsync((true, UniqueId));
        }
        catch
        {
            ValidationClass = "is-invalid";
            _ = IsValidChanged.InvokeAsync((false, UniqueId));
        }
    }
}
