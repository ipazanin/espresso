using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackTextFieldsSectionBlock : SlackSectionBlock
    {
        public SlackMarkdownTextBlock Text { get; }

        public IEnumerable<SlackMarkdownTextBlock> Fields { get; }

        public SlackTextFieldsSectionBlock(
            SlackMarkdownTextBlock text,
            IEnumerable<SlackMarkdownTextBlock> fields
        )
        {
            Text = text;
            Fields = fields;
        }
    }
}
