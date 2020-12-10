using System;
using System.IO;
using Espresso.Common.Constants;
using Espresso.Infrastructure.Persistence.Configuration;
using Espresso.Infrastructure.Persistence.DesignTime;
using Espresso.Infrastructure.Persistence.Enumerations;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    public class DatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<ApplicationDatabaseContext>
    {
        public DatabaseContextFactory()
            : base(
                new DesignTimeDatabaseContextFactoryConfiguration
                {
                    ConfigurationBasePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Espresso.WebApi/AppSettings/DatabaseSettings",
                    ConfigurationFileName = $"database-settings.{Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment)}.json",
                    ConnectionStringConfigurationPath = "DatabaseConfiguration:ConnectionString",
                    EnableSensitiveDataLogging = true,
                    EntityFrameworkDriver = EntityFrameworkDriver.SqlServer
                }
            )
        { }

        protected override ApplicationDatabaseContext CreateNewInstance(DbContextOptions<ApplicationDatabaseContext> options)
        {
            return new ApplicationDatabaseContext(options);
        }
    }
}
