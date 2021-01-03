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

        #region Constructors
        protected DesignTimeDatabaseContextFactoryBase()
        {
        }
        #endregion

        #region Methods
        public TContext CreateDbContext(string[] args)
        {
            var basePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Espresso.WebApi/AppSettings/DatabaseSettings";
            var configurationFilePath = $"database-settings.{Environment.GetEnvironmentVariable(EnvironmentVariableNamesConstants.AspNetCoreEnvironment)}.json";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(path: configurationFilePath, optional: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["DatabaseConfiguration:DefaultConnectionString"];

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
            System.Console.WriteLine("-------------------------------------------------------------");
            System.Console.WriteLine(connectionString);
            System.Console.WriteLine("-------------------------------------------------------------");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"{nameof(DesignTimeDatabaseContextFactoryBase<TContext>)}.{nameof(Create)}(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
        #endregion
    }
}
