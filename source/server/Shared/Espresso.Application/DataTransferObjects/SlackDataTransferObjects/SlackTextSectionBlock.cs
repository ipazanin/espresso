// SlackTextSectionBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackTextSectionBlock : SlackSectionBlock
    {
        /// <summary>
        /// Gets slack markdown text block.
        /// </summary>
        public SlackMarkdownTextBlock Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackTextSectionBlock"/> class.
        /// </summary>
        /// <param name="text">Markdown text block.</param>
        public SlackTextSectionBlock(SlackMarkdownTextBlock text)
        {
            Text = text;
        }
    }
}
