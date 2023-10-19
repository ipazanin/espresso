// RssFeedContentModifier.cs
//
// © 2022 Espresso News. All rights reserved.

#pragma warning disable RCS1170

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities;

public class RssFeedContentModifier : IEntity<int>
{
    public const int SourceValueMaxLength = 1000;
    public const int ReplacementValueMaxLength = 1000;

    /// <summary>
    /// Initializes a new instance of the <see cref="RssFeedContentModifier"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sourceValue"></param>
    /// <param name="replacementValue"></param>
    /// <param name="orderIndex"></param>
    /// <param name="rssFeedId"></param>
    public RssFeedContentModifier(
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

    public int Id { get; private set; }

    public string SourceValue { get; private set; }

    public string ReplacementValue { get; private set; }

    public int OrderIndex { get; private set; }

    public int RssFeedId { get; private set; }

    public RssFeed? RssFeed { get; private set; }
}
