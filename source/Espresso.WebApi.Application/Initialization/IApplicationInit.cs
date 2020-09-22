using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Initialization
{
    public interface IApplicationInit
    {
        public Task InitWebApi();

        public Task InitParserDeleter();
    }
}
