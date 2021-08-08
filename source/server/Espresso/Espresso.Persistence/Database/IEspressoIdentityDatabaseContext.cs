// IEspressoIdentityDatabaseContext.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Espresso.Persistence.Database
{
    public interface IEspressoIdentityDatabaseContext
    {
        public DatabaseFacade Database { get; }

        public DbSet<T> Set<T>()
            where T : class;

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
