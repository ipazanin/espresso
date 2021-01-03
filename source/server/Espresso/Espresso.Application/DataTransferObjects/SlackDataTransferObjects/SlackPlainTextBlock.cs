namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackPlainTextBlock : SlackBlock
    {
        public string Text { get; }

        public SlackPlainTextBlock(string text)
            : base(type: "plain_text")
        {
            Text = text;
        }
    }
}