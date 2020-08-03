using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepository;
using Microsoft.Data.SqlClient;

namespace Espresso.Persistence.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        #region Constants
        private const string TableName = "Articles";
        private const string IdColumnName = nameof(Article.Id);
        #endregion

        #region Fields

        private readonly IEnumerable<string> _columnNames = new[]
        {
                nameof(Article.Id),
                nameof(Article.ArticleId),
                nameof(Article.Url),
                nameof(Article.Summary),
                nameof(Article.Title),
                nameof(Article.ImageUrl),
                nameof(Article.CreateDateTime),
                nameof(Article.UpdateDateTime),
                nameof(Article.PublishDateTime),
                nameof(Article.NumberOfClicks),
                nameof(Article.TrendingScore),
                nameof(Article.NewsPortalId),
                nameof(Article.RssFeedId)
        };

        private readonly IEnumerable<string> _updateColumnNames = new[]
        {
                nameof(Article.ArticleId),
                nameof(Article.Url),
                nameof(Article.Summary),
                nameof(Article.Title),
                nameof(Article.ImageUrl)
        };

        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        #endregion

        #region Constructors
        public ArticleRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Article>> GetArticles()
        {
            var commandText = $"SELECT * FROM {TableName}";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            var articles = await connection.QueryAsync<Article>(
                sql: commandText
            );

            return articles;
        }

        public void InsertArticles(IEnumerable<Article> articles)
        {
            if (!articles.Any())
            {
                return;
            }

            var commandText = $"INSERT INTO {TableName} " +
                $"({string.Join(", ", _columnNames)}) " +
                $"VALUES " +
                $"({string.Join(", ", _columnNames.Select(articleColumnName => $"@{articleColumnName}"))})";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            connection.Open();

            command.CommandText = commandText;

            foreach (var article in articles)
            {
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Id)}", article.Id));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.ArticleId)}", article.ArticleId));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Url)}", article.Url));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Summary)}", article.Summary));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Title)}", article.Title));
                if (article.ImageUrl is null)
                {
                    command.Parameters.Add(new SqlParameter($"@{nameof(Article.ImageUrl)}", DBNull.Value));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter($"@{nameof(Article.ImageUrl)}", article.ImageUrl));
                }
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.CreateDateTime)}", article.CreateDateTime));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.UpdateDateTime)}", article.UpdateDateTime));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.PublishDateTime)}", article.PublishDateTime));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.NumberOfClicks)}", article.NumberOfClicks));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.TrendingScore)}", article.TrendingScore));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.NewsPortalId)}", article.NewsPortalId));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.RssFeedId)}", article.RssFeedId));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            connection.Close();
        }

        public void UpdateArticles(IEnumerable<Article> articles)
        {
            if (!articles.Any())
            {
                return;
            }
            var commandText = $"UPDATE {TableName} SET " +
                $"{string.Join(", ", _updateColumnNames.Select(articleColumnName => $"{articleColumnName} = @{articleColumnName}"))} " +
                $"WHERE {IdColumnName} = @{IdColumnName}";
            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            connection.Open();

            command.CommandText = commandText;

            foreach (var article in articles)
            {
                command.Parameters.Add(new SqlParameter($"@{IdColumnName}", article.Id));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.ArticleId)}", article.ArticleId));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Url)}", article.Url));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Summary)}", article.Summary));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Title)}", article.Title));
                if (article.ImageUrl is null)
                {
                    command.Parameters.Add(new SqlParameter($"@{nameof(Article.ImageUrl)}", DBNull.Value));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter($"@{nameof(Article.ImageUrl)}", article.ImageUrl));
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            connection.Close();
        }

        public int DeleteArticles(DateTime maxArticleAge)
        {
            var parameterName = $"@{nameof(Article.PublishDateTime)}";
            var commandText = $"DELETE FROM {TableName} WHERE {nameof(Article.PublishDateTime)} < {parameterName}";

            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.Add(new SqlParameter(parameterName, maxArticleAge));

            connection.Open();
            var numberOfDeletedRows = command.ExecuteNonQuery();
            connection.Close();

            return numberOfDeletedRows;
        }
        #endregion
    }
}
