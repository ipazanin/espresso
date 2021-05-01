using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackImageBlock : SlackBlock
    {
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; }

        [JsonPropertyName("alt_text")]
        public string AltText { get; }

        public SlackImageBlock(
            string imageUrl,
            string altText
        )
            : base(type: "image")
        {
            ImageUrl = imageUrl;
            AltText = altText;
        }
    }
}
