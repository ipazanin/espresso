// Startup.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Dashboard.Configuration;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Startup
{
    internal sealed partial class Startup
    {
        private readonly IDashboardConfiguration _dashboardConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _dashboardConfiguration = new DashboardConfiguration(configuration);
        }
    }
}
