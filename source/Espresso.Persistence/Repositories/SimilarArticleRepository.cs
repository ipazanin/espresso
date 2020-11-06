using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Microsoft.Data.SqlClient;

namespace Espresso.Persistence.Repositories
{
    public class SimilarArticleRepository : ISimilarArticleRepository
    {
        #region Constants
        private const string TableName = "SimilarArticles";
        #endregion

        #region Fields
        private readonly IEnumerable<string> _columnNames = new[]
        {
            nameof(SimilarArticle.Id),
            nameof(SimilarArticle.SimilarityScore),
            nameof(SimilarArticle.MainArticleId),
            nameof(SimilarArticle.SubordinateArticleId)
        };

        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        #endregion

        #region Constructors
        public SimilarArticleRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }
        #endregion

        #region Methods
        public void InsertSimilarArticles(IEnumerable<SimilarArticle> similarArticles)
        {
            if (!similarArticles.Any())
            {
                return;
            }
            var commandText = $"INSERT INTO {TableName} " +
                $"({string.Join(", ", _columnNames)}) " +
                $"VALUES " +
                $"({string.Join(", ", _columnNames.Select(columnName => $"@{columnName}"))})";

            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            connection.Open();

            command.CommandText = commandText;

            foreach (var similarArticle in similarArticles)
            {
                command.Parameters.Add(new SqlParameter($"@{nameof(SimilarArticle.Id)}", similarArticle.Id));
                command.Parameters.Add(new SqlParameter($"@{nameof(SimilarArticle.SimilarityScore)}", similarArticle.SimilarityScore));
                command.Parameters.Add(new SqlParameter($"@{nameof(SimilarArticle.MainArticleId)}", similarArticle.MainArticleId));
                command.Parameters.Add(new SqlParameter($"@{nameof(SimilarArticle.SubordinateArticleId)}", similarArticle.SubordinateArticleId));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            connection.Close();
        }
        #endregion
    }
}
