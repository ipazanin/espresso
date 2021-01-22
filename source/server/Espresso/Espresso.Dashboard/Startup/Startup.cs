using Espresso.Dashboard.Configuration;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Startup
{
    internal sealed partial class Startup
    {
        #region Fields
        private readonly IDashboardConfiguration _dashboardConfiguration;
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration)
        {
            _dashboardConfiguration = new DashboardConfiguration(configuration);
        }
        #endregion
    }
}
