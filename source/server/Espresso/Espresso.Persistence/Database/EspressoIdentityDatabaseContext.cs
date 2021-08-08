// EspressoIdentityDatabaseContext.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    public class EspressoIdentityDatabaseContext : IdentityDbContext, IEspressoIdentityDatabaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EspressoIdentityDatabaseContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public EspressoIdentityDatabaseContext(DbContextOptions<EspressoIdentityDatabaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
