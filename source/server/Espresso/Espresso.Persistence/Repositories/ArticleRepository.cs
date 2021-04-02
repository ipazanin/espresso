﻿using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Persistence.Database;
using Espresso.Persistence.IRepositories;
using Microsoft.Data.SqlClient;

namespace Espresso.Persistence.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        #region Constants
        private const string TableName = "Articles";
        private const string SimilarArticlesTableName = "SimilarArticles";

        private const string IdColumnName = nameof(Article.Id);
        private const string PublishDateTimeColumnName = nameof(Article.PublishDateTime);

        private const string MainArticleIdColumnName = nameof(SimilarArticle.MainArticleId);
        private const string SubordinateArticleIdColumnName = nameof(SimilarArticle.SubordinateArticleId);
        #endregion

        #region Fields
        private readonly IEnumerable<string> _columnNames = new[]
        {
                nameof(Article.Id),
                nameof(Article.Url),
                nameof(Article.WebUrl),
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
                nameof(Article.WebUrl),
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
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Url)}", article.Url));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.WebUrl)}", article.WebUrl));
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
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.Url)}", article.Url));
                command.Parameters.Add(new SqlParameter($"@{nameof(Article.WebUrl)}", article.WebUrl));
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

        public int DeleteArticlesAndSimilarArticles(DateTime maxArticleAge)
        {
            DeleteSimilarArticles(maxArticleAge);
            var numberOfDeletedRows = DeleteArticles(maxArticleAge);

            return numberOfDeletedRows;
        }

        private int DeleteArticles(DateTime maxArticleAge)
        {
            var parameterName = $"@{PublishDateTimeColumnName}";
            var commandText = $"DELETE FROM {TableName} WHERE {PublishDateTimeColumnName} < {parameterName}";

            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.Add(new SqlParameter(parameterName, maxArticleAge));

            connection.Open();
            var numberOfDeletedRows = command.ExecuteNonQuery();
            connection.Close();

            return numberOfDeletedRows;
        }

        private void DeleteSimilarArticles(DateTime maxArticleAge)
        {
            var parameterName = $"@{PublishDateTimeColumnName}";
            var commandText = $"DELETE FROM {SimilarArticlesTableName} " +
                $"WHERE {SimilarArticlesTableName}.{MainArticleIdColumnName} " +
                $"IN(SELECT {TableName}.{IdColumnName} FROM {TableName} WHERE {TableName}.{PublishDateTimeColumnName} < {parameterName}) " +
                $"OR {SimilarArticlesTableName}.{SubordinateArticleIdColumnName} " +
                $"IN(SELECT {TableName}.{IdColumnName} FROM {TableName} WHERE {TableName}.{PublishDateTimeColumnName} < {parameterName}) ";

            using var connection = _databaseConnectionFactory.CreateDatabaseConnection();
            using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.Add(new SqlParameter(parameterName, maxArticleAge));

            connection.Open();
            var numberOfDeletedRows = command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
    }
}