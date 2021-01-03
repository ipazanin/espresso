using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackTextSectionBlock : SlackSectionBlock
    {
        public SlackMarkdownTextBlock Text { get; }

        public SlackTextSectionBlock(SlackMarkdownTextBlock text)
        {
            Text = text;
        }
    }
}
