// DatabaseConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration;

public class DatabaseConfiguration
{
    private readonly IConfigurationSection _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConfiguration"/> class.
    /// </summary>
    /// <param name="configuration"></param>
    public DatabaseConfiguration(IConfigurationSection configuration)
    {
        _configuration = configuration;
    }

    public string EspressoDatabaseConnectionString => _configuration.GetValue<string>("EspressoDatabaseConnectionString")!;

    public string EspressoIdentityDatabaseConnectionString => _configuration.GetValue<string>("EspressoIdentityDatabaseConnectionString")!;

    public int CommandTimeoutInSeconds => _configuration.GetValue<int>("CommandTimeoutInSeconds");

    public QueryTrackingBehavior QueryTrackingBehavior => _configuration.GetValue<QueryTrackingBehavior>("QueryTrackingBehavior");

    public bool EnableDetailedErrors => _configuration.GetValue<bool>("EnableDetailedErrors");

    public bool EnableSensitiveDataLogging => _configuration.GetValue<bool>("EnableSensitiveDataLogging");
}
