namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackMarkdownTextBlock : SlackBlock
    {
        public string Text { get; }

        public SlackMarkdownTextBlock(string text)
            : base(type: "mrkdwn")
        {
            Text = text;
        }
    }
}