using System.Threading.Tasks;

namespace Espresso.Dashboard.Application.Initialization
{
    public interface IDashboardInit
    {
        public Task InitParserDeleter();
    }
}
