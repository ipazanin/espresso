// NewsPortalImage.cs
//
// © 2022 Espresso News. All rights reserved.

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

using Espresso.Domain.Infrastructure;

namespace Espresso.Domain.Entities;

public class NewsPortalImage : IEntity<int>
{
    public NewsPortalImage(
        int id,
        byte[]? imageBytes,
        int newsPortalId)
    {
        Id = id;
        ImageBytes = imageBytes;
        NewsPortalId = newsPortalId;
    }

    public int Id { get; private set; }

    public byte[]? ImageBytes { get; private set; }

    public int NewsPortalId { get; private set; }

    public NewsPortal? NewsPortal { get; private set; }
}
