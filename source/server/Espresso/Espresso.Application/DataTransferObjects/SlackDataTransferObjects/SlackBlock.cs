namespace Espresso.Application.DataTransferObjects.SlackDataTransferObjects
{
    /// <summary>
    /// Abstract record representing block
    /// </summary>
    /// <value></value>
    public abstract record SlackBlock
    {
        public string Type { get; }

        protected SlackBlock(string type)
        {
            Type = type;
        }
    }
}
