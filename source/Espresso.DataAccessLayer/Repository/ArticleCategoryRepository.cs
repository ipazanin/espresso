using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Espresso.DataAccessLayer.IRepository;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Microsoft.Data.SqlClient;

namespace Espresso.DataAccessLayer.Repository
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        #region Constants
        private const string TableName = "ArticleCategories";
        #endregion

        #region Fields
        private readonly IEnumerable<string> _articleCategoryNames = new[]
        {
                nameof(ArticleCategory.Id),
                nameof(ArticleCategory.CategoryId),
                nameof(ArticleCategory.ArticleId)
        };

        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        #endregion

        #region Constructors
        public ArticleCategoryRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ArticleCategory>> GetArticleCategories()
        {
            var commandText = $"SELECT * FROM {TableName}";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            var articleCategories = await connection.QueryAsync<ArticleCategory>(
                sql: commandText
            );

            return articleCategories;
        }

        public void InsertArticleCategories(IEnumerable<ArticleCategory> articleCategories)
        {
            var commandText = $"INSERT INTO {TableName} " +
                $"({string.Join(", ", _articleCategoryNames)}) " +
                $"VALUES " +
                $"({string.Join(", ", _articleCategoryNames.Select(articleCategoryName => $"@{articleCategoryName}"))})";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            connection.Open();

            command.CommandText = commandText;

            foreach (var articleCategory in articleCategories)
            {
                command.Parameters.Add(new SqlParameter($"@{nameof(ArticleCategory.Id)}", articleCategory.Id));
                command.Parameters.Add(new SqlParameter($"@{nameof(ArticleCategory.CategoryId)}", articleCategory.CategoryId));
                command.Parameters.Add(new SqlParameter($"@{nameof(ArticleCategory.ArticleId)}", articleCategory.ArticleId));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            connection.Close();
        }
        #endregion
    }
}
