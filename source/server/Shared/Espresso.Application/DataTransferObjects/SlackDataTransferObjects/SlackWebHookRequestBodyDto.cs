// SlackWebHookRequestBodyDto.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackWebHookRequestBodyDto
    {
        /// <summary>
        /// Gets slack username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets message icon emoji.
        /// </summary>
        [JsonPropertyName("icon_emoji")]
        public string IconEmoji { get; }

        /// <summary>
        /// Gets message text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets message blocks.
        /// </summary>
        public IEnumerable<object> Blocks { get; }

        /// <summary>
        /// Gets channel name.
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackWebHookRequestBodyDto"/> class.
        /// </summary>
        /// <param name="userName">Sender username.</param>
        /// <param name="iconEmoji">Sender icon emoji.</param>
        /// <param name="text">Message text.</param>
        /// <param name="channel">Receiving channel.</param>
        /// <param name="blocks">Message blocks.</param>
        public SlackWebHookRequestBodyDto(
            string userName,
            string iconEmoji,
            string text,
            string channel,
            IEnumerable<object> blocks)
        {
            Username = userName;
            IconEmoji = iconEmoji;
            Text = text;
            Channel = channel;
            Blocks = blocks;
        }
    }
}
