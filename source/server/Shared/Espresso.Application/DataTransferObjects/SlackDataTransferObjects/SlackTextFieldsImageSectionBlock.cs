// SlackTextFieldsImageSectionBlock.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackTextFieldsImageSectionBlock : SlackSectionBlock
    {
        /// <summary>
        /// Gets slack markdown text block.
        /// </summary>
        public SlackMarkdownTextBlock Text { get; }

        /// <summary>
        /// Gets slack markdown text blocks.
        /// </summary>
        public IEnumerable<SlackMarkdownTextBlock> Fields { get; }

        /// <summary>
        /// Gets slack image block.
        /// </summary>
        public SlackImageBlock Accessory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackTextFieldsImageSectionBlock"/> class.
        /// </summary>
        /// <param name="text">Markdown text block.</param>
        /// <param name="fields">Markdown text blocks.</param>
        /// <param name="accessory">Slack image block.</param>
        public SlackTextFieldsImageSectionBlock(
            SlackMarkdownTextBlock text,
            IEnumerable<SlackMarkdownTextBlock> fields,
            SlackImageBlock accessory)
        {
            Text = text;
            Fields = fields;
            Accessory = accessory;
        }
    }
}
