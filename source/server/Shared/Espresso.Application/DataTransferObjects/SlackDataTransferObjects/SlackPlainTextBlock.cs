// SlackPlainTextBlock.cs
//
// © 2021 Espresso News. All rights reserved.

namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects;

public record SlackPlainTextBlock : SlackBlock
{
    /// <summary>
    /// gets text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SlackPlainTextBlock"/> class.
    /// </summary>
    /// <param name="text">Text.</param>
    public SlackPlainTextBlock(string text)
        : base(type: "plain_text")
    {
        Text = text;
    }
}
