using Espresso.ParserDeleter.Configuration;
using Microsoft.Extensions.Configuration;

namespace Espresso.ParserDeleter.Startup
{
    internal sealed partial class Startup
    {
        #region Fields
        private readonly IParserDeleterConfiguration _parserDeleterConfiguration;
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration)
        {
            _parserDeleterConfiguration = new ParserDeleterConfiguration(configuration);
        }
        #endregion
    }
}
