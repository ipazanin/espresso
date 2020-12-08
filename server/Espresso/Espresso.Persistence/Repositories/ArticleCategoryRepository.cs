using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Microsoft.Data.SqlClient;

namespace Espresso.Persistence.Repositories
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        #region Constants
        private const string TableName = "ArticleCategories";
        #endregion

        #region Fields
        private readonly IEnumerable<string> _articleCategoryColumnNames = new[]
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
            if (!articleCategories.Any())
            {
                return;
            }
            var commandText = $"INSERT INTO {TableName} " +
                $"({string.Join(", ", _articleCategoryColumnNames)}) " +
                $"VALUES " +
                $"({string.Join(", ", _articleCategoryColumnNames.Select(articleCategoryName => $"@{articleCategoryName}"))})";

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

        public void DeleteArticleCategories(IEnumerable<Guid> articleCategoryIds)
        {
            if (!articleCategoryIds.Any())
            {
                return;
            }

            var i = 0;
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();

            var articleCategoryIdParameterNames = new List<string>();

            foreach (var articleCategoryId in articleCategoryIds)
            {
                var parameterName = $"@{nameof(ArticleCategory.Id)}{i}";
                articleCategoryIdParameterNames.Add(parameterName);
                command.Parameters.Add(new SqlParameter(parameterName, articleCategoryId));
                i++;
            }

            var articleCategoryIdsCommaDelimited = string.Join(", ", articleCategoryIdParameterNames);
            var commandText = $"DELETE FROM {TableName} WHERE {nameof(ArticleCategory.Id)} IN({articleCategoryIdsCommaDelimited})";

            command.CommandText = commandText;

            connection.Open();
            var numberOfDeletedRows = command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion
    }
}
