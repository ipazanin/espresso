// SendInformationToApiRabbitMqService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Common.Services.Contracts;
using Espresso.Dashboard.Application.IServices;
using RabbitMQ.Client;

namespace Espresso.Dashboard.Services;

public class SendInformationToApiRabbitMqService : ISendInformationToApiService
{
    private readonly IJsonService _jsonService;
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendInformationToApiRabbitMqService"/> class.
    /// </summary>
    /// <param name="jsonService"></param>
    /// <param name="hostName"></param>
    /// <param name="queueName"></param>
    /// <param name="port"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public SendInformationToApiRabbitMqService(
        IJsonService jsonService,
        string hostName,
        string queueName,
        int port,
        string username,
        string password)
    {
        _jsonService = jsonService;
        _hostName = hostName;
        _queueName = queueName;
        _port = port;
        _username = username;
        _password = password;
    }

    public Task SendSettingUpdatedNotification(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SendArticlesMessage(
        IEnumerable<Guid> createArticleIds,
        IEnumerable<Guid> updateArticleIds,
        CancellationToken cancellationToken)
    {
        if (!createArticleIds.Any() && !updateArticleIds.Any())
        {
            return;
        }

        var articlesBodyUtf8Bytes = await GetMessageBody(
            createArticleIds: createArticleIds,
            updateArticleIds: updateArticleIds,
            cancellationToken: cancellationToken);

        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            Port = _port,
            UserName = _username,
            Password = _password,
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var properties = channel.CreateBasicProperties();

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: _queueName,
            basicProperties: properties,
            body: articlesBodyUtf8Bytes);
    }

    private async Task<byte[]> GetMessageBody(
        IEnumerable<Guid> createArticleIds,
        IEnumerable<Guid> updateArticleIds,
        CancellationToken cancellationToken)
    {
        var data = new ArticlesBodyDto(
            createdArticleIds: createArticleIds,
            updatedArticleIds: updateArticleIds);

        var articlesBodyJson = await _jsonService.Serialize(data, cancellationToken);
        var articlesBodyUtf8Bytes = Encoding.UTF8.GetBytes(articlesBodyJson);

        return articlesBodyUtf8Bytes;
    }
}
