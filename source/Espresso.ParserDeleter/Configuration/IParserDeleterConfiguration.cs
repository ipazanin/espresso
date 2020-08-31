using System;
using Espresso.Common.Configuration;

namespace Espresso.ParserDeleter.Configuration
{
    public interface IParserDeleterConfiguration : ICommonConfiguration
    {
        public ApiKeysConfiguration ApiKeysConfiguration { get; }
        public AppConfiguration AppConfiguration { get; }
        public DatabaseConfiguration DatabaseConfiguration { get; }
        public TimersConfiguration TimersConfiguration { get; }
    }
}
