using System.Threading.Tasks;

namespace Espresso.Application.Initialization
{
    public interface IMemoryCacheInit
    {
        public Task InitWebApi();

        public Task InitParserDeleter();
    }
}
