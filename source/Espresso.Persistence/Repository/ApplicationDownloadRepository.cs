using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepository;

namespace Espresso.Persistence.Repository
{
    public class ApplicationDownloadRepository : IApplicationDownloadRepository
    {
        #region Constants
        private const string TableName = "ApplicationDownload";
        #endregion

        #region Fields
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        #endregion

        #region Constructors
        public ApplicationDownloadRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ApplicationDownload>> GetApplicationDownloads()
        {
            var commandText = $"SELECT * FROM {TableName}";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            var applicationDownloads = await connection.QueryAsync<ApplicationDownload>(
                sql: commandText
            );

            return applicationDownloads;
        }
        #endregion
    }
}
