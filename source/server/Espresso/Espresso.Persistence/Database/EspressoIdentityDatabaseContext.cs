using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Espresso.Persistence.Database
{
    public class EspressoIdentityDatabaseContext : IdentityDbContext, IEspressoIdentityDatabaseContext
    {
        #region Constructors
        public EspressoIdentityDatabaseContext(DbContextOptions<EspressoIdentityDatabaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
