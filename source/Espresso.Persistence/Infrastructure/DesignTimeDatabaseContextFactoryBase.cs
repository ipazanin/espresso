using System;
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
        public DesignTimeDatabaseContextFactoryBase()
        {
        }

        public TContext CreateDbContext(string[] args)
        {
            var basePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Espresso.WebApi";
            return Create(
                basePath: basePath,
                environmentName: Environment.GetEnvironmentVariable(EnviromentVariableNamesConstants.AspNetCoreEnvironment) ?? ""
            );
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string DefaultConnectionString is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"{nameof(DesignTimeDatabaseContextFactoryBase<TContext>)}.{nameof(Create)}(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(connectionString);
            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
