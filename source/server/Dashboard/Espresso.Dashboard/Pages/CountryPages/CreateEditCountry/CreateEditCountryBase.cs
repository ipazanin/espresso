// CreateEditCountryBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.CountryDataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Espresso.Dashboard.Pages.CountryPages.CreateEditCountry;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateEditCountryBase : ComponentBase
{
    private bool _success;

    [Parameter]
    [EditorRequired]
    public EventCallback<MouseEventArgs> OnSaveButtonClick { get; set; }

    [Parameter]
    [EditorRequired]
    public CountryDto Country { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public CountryImageDto CountryImage { get; set; } = null!;

    [Parameter]
    public EventCallback<bool> SuccessChanged { get; set; }

#pragma warning disable BL0007 // Component parameter should be auto property
    [Parameter]
    public bool Success
    {
        get => _success;
        set
        {
            if (_success == value)
            {
                return;
            }

            _success = value;
            _ = SuccessChanged.InvokeAsync(value);
        }
    }
#pragma warning restore BL0007 // Component parameter should be auto property

    protected string[] Errors { get; set; } = Array.Empty<string>();

    protected MudForm? Form { get; set; }

    protected async Task OnUploadIcon(IBrowserFile databaseFile)
    {
        if (Country is null)
        {
            return;
        }

        var fileStream = databaseFile.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        CountryImage = new CountryImageDto(
            id: CountryImage.Id,
            imageBytes: memoryStream.ToArray(),
            relativeUrl: CountryImage.RelativeUrl,
            countryId: Country.Id);
    }
}
