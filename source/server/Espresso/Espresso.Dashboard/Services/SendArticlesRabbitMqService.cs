using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Common.Services.Contracts;
using Espresso.Domain.Entities;
using Espresso.Dashboard.Application.IServices;
using RabbitMQ.Client;

namespace Espresso.Dashboard.Services
{
    public class SendArticlesRabbitMqService : ISendArticlesService
    {
        private readonly IJsonService _jsonService;
        #region Fields
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        #endregion

        #region Constructors
        public SendArticlesRabbitMqService(
            IJsonService jsonService,
            string hostName,
            string queueName,
            int port,
            string username,
            string password
        )
        {
            _jsonService = jsonService;
            _hostName = hostName;
            _queueName = queueName;
            _port = port;
            _username = username;
            _password = password;
        }
        #endregion

        #region Methods
        public async Task SendArticlesMessage(
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        )
        {
            if (!createArticles.Any() && !updateArticles.Any())
            {
                return;
            }

            var articlesBodyUtf8Bytes = await GetMessageBody(
                createArticles: createArticles,
                updateArticles: updateArticles,
                cancellationToken: cancellationToken
            );

            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port,
                UserName = _username,
                Password = _password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var properties = channel.CreateBasicProperties();

            channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: properties,
                body: articlesBodyUtf8Bytes
            );
        }

        private async Task<byte[]> GetMessageBody(
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        )
        {
            var articleDtoProjection = ArticleDto.GetProjection().Compile();

            var data = new ArticlesBodyDto(
                createdArticles: createArticles.Select(articleDtoProjection),
                updatedArticles: updateArticles.Select(articleDtoProjection)
            );

            var articlesBodyJson = await _jsonService.Serialize(data, cancellationToken);
            var articlesBodyUtf8Bytes = Encoding.UTF8.GetBytes(articlesBodyJson);

            return articlesBodyUtf8Bytes;
        }
        #endregion
    }
}
