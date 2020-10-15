using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects
{
    public class SlackWebHookDto
    {
        [JsonPropertyName("username")]
        public string Username { get; }

        [JsonPropertyName("icon_emoji")]
        public string IconEmoji { get; }

        [JsonPropertyName("text")]
        public string Text { get; }

        [JsonPropertyName("channel")]
        public string Channel { get; }

        public SlackWebHookDto(
            string userName,
            string iconEmoji,
            string text,
            string channel
        )
        {
            Username = userName;
            IconEmoji = iconEmoji;
            Text = text;
            Channel = channel;
        }
    }
}
