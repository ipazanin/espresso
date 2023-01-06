// CreateEditRssFeedBase.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;
using Espresso.Dashboard.Application.RssFeeds.Queries.GetRssFeedsDetails;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Espresso.Dashboard.Pages.RssFeedPages.CreateEditRssFeed;

/// <summary>
/// NewsPortalDetailsBase.
/// </summary>
public class CreateEditRssFeedBase : ComponentBase
{
    private bool _success;

    [Parameter]
    [EditorRequired]
    public EventCallback<MouseEventArgs> OnSaveButtonClick { get; set; }

    [Parameter]
    [EditorRequired]
    public GetRssFeedDetailsQueryResponse? RssFeedDetails { get; set; }

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

    protected void OnDeleteRssFeedCategoryButtonClicked(int rssFeedCategoryId)
    {
        if (RssFeedDetails is null)
        {
            return;
        }

        var rssFeedCategoryDtoToRemove = RssFeedDetails.RssFeedCategories.First(rssFeedCategory => rssFeedCategory.Id == rssFeedCategoryId);
        RssFeedDetails.RssFeedCategories.Remove(rssFeedCategoryDtoToRemove);
    }

    protected void OnAddRssFeedCategoryButtonClicked()
    {
        if (RssFeedDetails is null)
        {
            return;
        }

        var rssFeedCategory = new RssFeedCategoryDto(
            id: default,
            urlRegex: string.Empty,
            urlSegmentIndex: default,
            categoryId: RssFeedDetails.RssFeed.CategoryId,
            rssFeedId: RssFeedDetails.RssFeed.Id);

        RssFeedDetails.RssFeedCategories.Add(rssFeedCategory);
    }

    protected void OnDeleteRssFeedContentModifierButtonClicked(int rssFeedContentModifierId)
    {
        if (RssFeedDetails is null)
        {
            return;
        }

        var rssFeedContentModifierToRemove = RssFeedDetails
            .RssFeedContentModifiers
            .First(rssFeedContentModifier => rssFeedContentModifier.Id == rssFeedContentModifierId);

        RssFeedDetails.RssFeedContentModifiers.Remove(rssFeedContentModifierToRemove);
    }

    protected void OnAddRssFeedContentModifierButtonClicked()
    {
        if (RssFeedDetails is null)
        {
            return;
        }

        var rssFeedContentModifier = new RssFeedContentModifierDto(
            id: default,
            sourceValue: string.Empty,
            replacementValue: string.Empty,
            orderIndex: 1,
            rssFeedId: RssFeedDetails.RssFeed.Id);

        RssFeedDetails.RssFeedContentModifiers.Add(rssFeedContentModifier);
    }
}
