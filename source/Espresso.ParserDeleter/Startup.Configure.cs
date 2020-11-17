using Espresso.ParserDeleter.Application.Initialization;

namespace Espresso.ParserDeleter
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        public static void Configure(
            IParserDeleterInit memoryCacheInit
        )
        {
            memoryCacheInit.InitParserDeleter().GetAwaiter().GetResult();
        }
    }
}
