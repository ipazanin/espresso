using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackWebHookRequestBodyDto
    {
        // [JsonPropertyName("username")]
        public string Username { get; }

        [JsonPropertyName("icon_emoji")]
        public string IconEmoji { get; }

        // [JsonPropertyName("text")]
        public string Text { get; }

        // [JsonPropertyName("blocks")]
        public IEnumerable<object> Blocks { get; }

        // [JsonPropertyName("channel")]
        public string Channel { get; }

        public SlackWebHookRequestBodyDto(
            string userName,
            string iconEmoji,
            string text,
            string channel,
            IEnumerable<object> blocks
        )
        {
            Username = userName;
            IconEmoji = iconEmoji;
            Text = text;
            Channel = channel;
            Blocks = blocks;
        }
    }
}
