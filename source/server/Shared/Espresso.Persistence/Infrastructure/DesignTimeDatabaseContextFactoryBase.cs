// DesignTimeDatabaseContextFactoryBase.cs
//
// © 2021 Espresso News. All rights reserved.

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Espresso.Persistence.Infrastructure
{
    public abstract class DesignTimeDatabaseContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        private const string EspressoDatabaseContextName = "EspressoDatabaseContext";
        private const string EspressoDatabaseContextConfigurationPath = "DatabaseConfiguration:EspressoDatabaseConnectionString";

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignTimeDatabaseContextFactoryBase{TContext}"/> class.
        /// </summary>
        protected DesignTimeDatabaseContextFactoryBase()
        {
        }

        public TContext CreateDbContext(string[] args)
        {
            var connectionString = args.Length != 1 ?
                throw new Exception("Connection String must be specified as first argument!") :
                args[0];

            return Create(connectionString);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        /// <summary>
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
    }
}
