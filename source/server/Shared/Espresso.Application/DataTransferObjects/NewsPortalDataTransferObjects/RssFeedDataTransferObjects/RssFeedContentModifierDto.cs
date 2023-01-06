// RssFeedContentModifierDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects.RssFeedDataTransferObjects;

public class RssFeedContentModifierDto
{
    public RssFeedContentModifierDto(
        int id,
        string sourceValue,
        string replacementValue,
        int orderIndex,
        int rssFeedId)
    {
        Id = id;
        SourceValue = sourceValue;
        ReplacementValue = replacementValue;
        OrderIndex = orderIndex;
        RssFeedId = rssFeedId;
    }

    private RssFeedContentModifierDto()
    {
    }

    public static Expression<Func<RssFeedContentModifier, RssFeedContentModifierDto>> Projection
    {
        get => rssFeedContentModifier => new RssFeedContentModifierDto
        {
            Id = rssFeedContentModifier.Id,
            SourceValue = rssFeedContentModifier.SourceValue,
            ReplacementValue = rssFeedContentModifier.ReplacementValue,
            OrderIndex = rssFeedContentModifier.OrderIndex,
            RssFeedId = rssFeedContentModifier.RssFeedId,
        };
    }

    public int Id { get; set; }

    public string SourceValue { get; set; } = string.Empty;

    public string ReplacementValue { get; set; } = string.Empty;

    public int OrderIndex { get; set; }

    public int RssFeedId { get; set; }

    public RssFeedContentModifier CreateRssFeedContentModifier()
    {
        return new RssFeedContentModifier(
            id: Id,
            sourceValue: SourceValue,
            replacementValue: ReplacementValue,
            orderIndex: OrderIndex,
            rssFeedId: RssFeedId);
    }
}
