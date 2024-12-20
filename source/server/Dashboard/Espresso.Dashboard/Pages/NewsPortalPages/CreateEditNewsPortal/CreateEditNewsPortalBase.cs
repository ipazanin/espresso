﻿// CreateEditNewsPortalBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;
using Espresso.Common.Constants;
using Espresso.Dashboard.Application.NewsPortalImage.Queries.GetNewsPortalImage;
using Espresso.Dashboard.Application.NewsPortals.Queries.GetNewsPortalDetails;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Espresso.Dashboard.Pages.NewsPortalPages.CreateEditNewsPortal;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateEditNewsPortalBase : ComponentBase
{
    private bool _success;

    [Inject]
    public IServiceScopeFactory ServiceScopeFactory { get; init; } = null!;

    [Parameter]
    [EditorRequired]
    public EventCallback<MouseEventArgs> OnSaveButtonClick { get; set; }

    [Parameter]
    [EditorRequired]
    public GetNewsPortalDetailsQueryResponse? NewsPortalDetailsResponse { get; set; }

    [Parameter]
    [EditorRequired]
    public GetNewsPortalImageQueryResponse? NewsPortalImageResponse { get; set; }

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

#pragma warning disable CA1819 // Properties should not return arrays
    protected string[] Errors { get; set; } = [];
#pragma warning restore CA1819 // Properties should not return arrays

    protected MudForm? Form { get; set; }

    protected Func<string?, string?> IconUrlValidation { get; } = new Func<string?, string?>(iconUrlValue =>
    {
        if (string.IsNullOrWhiteSpace(iconUrlValue))
        {
            return "Icon URL must be defined";
        }

        if (!iconUrlValue.EndsWith(FileExtensionConstants.Png, StringComparison.OrdinalIgnoreCase) && !iconUrlValue.EndsWith(FileExtensionConstants.Jpg, StringComparison.Ordinal))
        {
            return $"Icon URL must end with {FileExtensionConstants.Png} or {FileExtensionConstants.Jpg}";
        }

        if (!iconUrlValue.StartsWith("Icons/", StringComparison.OrdinalIgnoreCase))
        {
            return "Are you sure you want to create icon outside Icons/ folder?";
        }

        return null;
    });

    protected async Task OnUploadIcon(IBrowserFile databaseFile)
    {
        if (NewsPortalDetailsResponse is null || NewsPortalImageResponse is null)
        {
            return;
        }

        var fileStream = databaseFile.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        NewsPortalImageResponse.NewsPortalImage = new NewsPortalImageDto(
            id: NewsPortalImageResponse.NewsPortalImage?.Id ?? default,
            imageBytes: memoryStream.ToArray(),
            newsPortalId: NewsPortalDetailsResponse.NewsPortal.Id);
    }
}
