namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    public abstract record SlackSectionBlock : SlackBlock
    {
        protected SlackSectionBlock()
            : base("section")
        {
        }
    }
}
