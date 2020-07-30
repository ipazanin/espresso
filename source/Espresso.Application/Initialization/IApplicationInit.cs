using System.Threading.Tasks;

namespace Espresso.Application.Initialization
{
    public interface IApplicationInit
    {
        public Task InitWebApi();

        public Task InitParserDeleter();
    }
}
