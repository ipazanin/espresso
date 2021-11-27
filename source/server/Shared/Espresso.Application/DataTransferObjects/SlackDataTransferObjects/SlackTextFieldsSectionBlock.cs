// SlackTextFieldsSectionBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects;

public record SlackTextFieldsSectionBlock : SlackSectionBlock
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
    /// Initializes a new instance of the <see cref="SlackTextFieldsSectionBlock"/> class.
    /// </summary>
    /// <param name="text">Markdown text block.</param>
    /// <param name="fields">Markdown text blocks.</param>
    public SlackTextFieldsSectionBlock(
        SlackMarkdownTextBlock text,
        IEnumerable<SlackMarkdownTextBlock> fields)
    {
        Text = text;
        Fields = fields;
    }
}
