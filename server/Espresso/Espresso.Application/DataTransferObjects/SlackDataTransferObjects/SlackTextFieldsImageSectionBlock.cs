using System.Collections.Generic;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackTextFieldsImageSectionBlock : SlackSectionBlock
    {
        public SlackMarkdownTextBlock Text { get; }

        public IEnumerable<SlackMarkdownTextBlock> Fields { get; }

        public SlackImageBlock Accessory { get; }

        public SlackTextFieldsImageSectionBlock(
            SlackMarkdownTextBlock text,
            IEnumerable<SlackMarkdownTextBlock> fields,
            SlackImageBlock accessory
        )
        {
            Text = text;
            Fields = fields;
            Accessory = accessory;
        }
    }
}
