// NewsPortalImageDto.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Linq.Expressions;
using Espresso.Domain.Entities;

namespace Espresso.Application.DataTransferObjects.NewsPortalDataTransferObjects;

public sealed class NewsPortalImageDto
{
    public NewsPortalImageDto(int id, byte[]? imageBytes, int newsPortalId)
    {
        Id = id;
        ImageBytes = imageBytes;
        NewsPortalId = newsPortalId;
    }

    private NewsPortalImageDto()
    {
    }

    public int Id { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    public byte[]? ImageBytes { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public int NewsPortalId { get; set; }

    public static Expression<Func<NewsPortalImage, NewsPortalImageDto>> GetProjection()
    {
        return newsPortalImage => new NewsPortalImageDto
        {
            Id = newsPortalImage.Id,
            ImageBytes = newsPortalImage.ImageBytes,
            NewsPortalId = newsPortalImage.NewsPortalId,
        };
    }

    public NewsPortalImage CreateNewsPortalImage()
    {
        return new NewsPortalImage(
            id: Id,
            imageBytes: ImageBytes,
            newsPortalId: NewsPortalId);
    }
}
