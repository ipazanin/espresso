using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Espresso.Persistence.Database
{
    public interface IEspressoIdentityDatabaseContext
    {
        #region Properties
        public DatabaseFacade Database { get; }
        #endregion Properties

        #region Methods
        public DbSet<T> Set<T>() where T : class;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion Methods
    }
}
