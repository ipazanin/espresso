// SlackHeaderBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackHeaderBlock : SlackBlock
    {
        /// <summary>
        /// Gets <see cref="SlackPlainTextBlock"/>.
        /// </summary>
        public SlackPlainTextBlock Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackHeaderBlock"/> class.
        /// </summary>
        /// <param name="text">Text.</param>
        public SlackHeaderBlock(SlackPlainTextBlock text)
            : base("header")
        {
            Text = text;
        }
    }
}
