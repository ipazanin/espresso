﻿using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Infrastructure
{
    public class DatabaseContextFactory : DesignTimeDatabaseContextFactoryBase<EspressoDatabaseContext>
    {
        protected override EspressoDatabaseContext CreateNewInstance(DbContextOptions<EspressoDatabaseContext> options)
        {
            return new EspressoDatabaseContext(options);
        }
    }
}
