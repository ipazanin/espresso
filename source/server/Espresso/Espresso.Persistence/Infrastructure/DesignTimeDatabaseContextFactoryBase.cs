using System;
using System.Collections;
using System.IO;
using Espresso.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Espresso.Persistence.Infrastructure
{
    public abstract class DesignTimeDatabaseContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        #region Constants
        private const string EspressoDatabaseContextName = "EspressoDatabaseContext";
        private const string EspressoDatabaseContextConfigurationPath = "DatabaseConfiguration:EspressoDatabaseConnectionString";
        #endregion

        #region Constructors
        protected DesignTimeDatabaseContextFactoryBase()
        {
        }
        #endregion

        #region Methods
        public TContext CreateDbContext(string[] args)
        {
            // var basePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Espresso.Dashboard/AppSettings/DatabaseSettings";
            // var configurationFilePath = $"database-settings.{Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment)}.json";
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(basePath)
            //     .AddJsonFile(path: configurationFilePath, optional: false)
            //     .AddEnvironmentVariables()
            //     .Build();

            // var connectionString = configuration[EspressoDatabaseContextConfigurationPath];

            var connectionString = args.Length != 1 ?
                throw new Exception("Connection String must be specified as first argument!") :
                args[0];

            return Create(connectionString);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        /// <summary>
        /// TODO: Add multiple providers
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"{nameof(DesignTimeDatabaseContextFactoryBase<TContext>)}.{nameof(Create)}(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseNpgsql(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
        #endregion
    }
}
