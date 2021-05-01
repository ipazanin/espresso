namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackHeaderBlock : SlackBlock
    {
        public SlackPlainTextBlock Text { get; }
        public SlackHeaderBlock(SlackPlainTextBlock text)
            : base("header")
        {
            Text = text;
        }
    }
}
