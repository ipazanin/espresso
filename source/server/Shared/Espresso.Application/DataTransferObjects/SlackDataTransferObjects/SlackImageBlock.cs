// SlackImageBlock.cs
//
// © 2022 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects;

public record SlackImageBlock : SlackBlock
{
    /// <summary>
    /// Gets image url.
    /// </summary>
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; }

    /// <summary>
    /// Gets image alternative text.
    /// </summary>
    [JsonPropertyName("alt_text")]
    public string AltText { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackImageBlock"/> class.
    /// </summary>
    /// <param name="imageUrl">Image url.</param>
    /// <param name="altText">Image alternative text.</param>
    public SlackImageBlock(
        string imageUrl,
        string altText)
        : base(type: "image")
    {
        ImageUrl = imageUrl;
        AltText = altText;
    }
}
