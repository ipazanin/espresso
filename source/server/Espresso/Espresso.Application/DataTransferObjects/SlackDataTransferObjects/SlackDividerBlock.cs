namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public record SlackDividerBlock : SlackBlock
    {
        public SlackDividerBlock()
            : base("divider")
        {
        }
    }
}
