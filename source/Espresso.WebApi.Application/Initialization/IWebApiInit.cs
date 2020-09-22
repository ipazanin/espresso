using System.Threading.Tasks;

namespace Espresso.WebApi.Application.Initialization
{
    public interface IWebApiInit
    {
        public Task InitWebApi();
    }
}
