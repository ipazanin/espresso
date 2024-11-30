// NewsPortalImage.cs
//
// © 2022 Espresso News. All rights reserved.

#pragma warning disable RCS1170 // Use read-only auto-implemented property.
#pragma warning disable S1144 // Unused private types or members should be removed

namespace Espresso.Domain.Entities;

public class NewsPortalImage
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

#pragma warning disable CA1819 // Properties should not return arrays
    public byte[]? ImageBytes { get; private set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public int NewsPortalId { get; private set; }

    public NewsPortal? NewsPortal { get; private set; }
}
