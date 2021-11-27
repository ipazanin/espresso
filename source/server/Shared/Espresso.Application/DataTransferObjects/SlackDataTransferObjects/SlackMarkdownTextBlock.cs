// SlackMarkdownTextBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects;

public record SlackMarkdownTextBlock : SlackBlock
{
    /// <summary>
    /// Gets text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackMarkdownTextBlock"/> class.
    /// </summary>
    /// <param name="text">Text.</param>
    public SlackMarkdownTextBlock(string text)
        : base(type: "mrkdwn")
    {
        Text = text;
    }
}
