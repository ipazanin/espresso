// RssFeedItem.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Xml.Linq;
using Espresso.Domain.Entities;

namespace Espresso.Domain.Records;

public class RssFeedItem
{
    public string? Id { get; set; }

    public IEnumerable<Uri?>? Links { get; set; }

    public string? Title { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public DateTimeOffset PublishDateTime { get; set; }

    public IEnumerable<XElement> ElementExtensions { get; set; } = Enumerable.Empty<XElement>();

    public RssFeed RssFeed { get; set; } = null!;
}
