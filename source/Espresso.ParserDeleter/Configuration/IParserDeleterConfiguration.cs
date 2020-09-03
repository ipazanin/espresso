namespace Espresso.ParserDeleter.Configuration
{
    public interface IParserDeleterConfiguration
    {
        public ApiKeysConfiguration ApiKeysConfiguration { get; }
        public AppConfiguration AppConfiguration { get; }
        public DatabaseConfiguration DatabaseConfiguration { get; }
        public DateTimeConfiguration DateTimeConfiguration { get; }
    }
}
