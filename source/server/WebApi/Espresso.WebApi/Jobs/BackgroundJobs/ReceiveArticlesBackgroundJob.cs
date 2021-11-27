// ReceiveArticlesBackgroundJob.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Application.DataTransferObjects.ArticleDataTransferObjects;
using Espresso.Application.Infrastructure.BackgroundJobsInfrastructure;
using Espresso.Common.Enums;
using Espresso.Common.Services.Contracts;
using Espresso.Domain.IServices;
using Espresso.WebApi.Application.Articles.Commands.UpdateInMemoryArticles;
using Espresso.WebApi.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Espresso.WebApi.Services;

/// <summary>
///
/// </summary>
public class ReceiveArticlesBackgroundJob : BackgroundJob<ReceiveArticlesBackgroundJob>
{
    private readonly IWebApiConfiguration _webApiConfiguration;
    private readonly IJsonService _jsonService;
    private readonly string _queueName;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReceiveArticlesBackgroundJob"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="webApiConfiguration"></param>
    /// <param name="jsonService"></param>
    /// <param name="hostName"></param>
    /// <param name="queueName"></param>
    /// <param name="port"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public ReceiveArticlesBackgroundJob(
        IServiceScopeFactory serviceScopeFactory,
        IWebApiConfiguration webApiConfiguration,
        IJsonService jsonService,
        string hostName,
        string queueName,
        int port,
        string userName,
        string password)
        : base(serviceScopeFactory: serviceScopeFactory)
    {
        _webApiConfiguration = webApiConfiguration;
        _jsonService = jsonService;
        _queueName = queueName;

        var factory = new ConnectionFactory()
        {
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = password,
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _consumer = new EventingBasicConsumer(_channel);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cancellationToken"></param>
    public override Task DoWork(CancellationToken cancellationToken)
    {
#pragma warning disable AsyncFixer03 // Fire-and-forget async-void methods or delegates
#pragma warning disable VSTHRD101 // Avoid unsupported async delegates
        _consumer.Received += async (model, basicDeliveryArguments) =>
        {
            var body = basicDeliveryArguments.Body.ToArray();
            var articlesBody = await _jsonService.Deserialize<ArticlesBodyDto>(body, cancellationToken);

            if (articlesBody is not null)
            {
                using var scope = ServiceScopeFactory.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                try
                {
                    await sender.Send(new UpdateInMemoryArticlesCommand
                    {
                        CreatedArticles = articlesBody.CreatedArticles,
                        UpdatedArticles = articlesBody.UpdatedArticles,
                        TargetedApiVersion = _webApiConfiguration.AppConfiguration.ApiVersion.ToString(),
                        ConsumerVersion = _webApiConfiguration.AppConfiguration.Version,
                        DeviceType = DeviceType.RssFeedParser,
                    });
                    _channel.BasicAck(deliveryTag: basicDeliveryArguments.DeliveryTag, false);
                }
                catch (Exception exception)
                {
                    var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService<ReceiveArticlesBackgroundJob>>();

                    loggerService.Log(
                        eventName: "ReceiveArticlesBackgroundJob Error",
                        exception: exception,
                        logLevel: LogLevel.Error);
                    _channel.BasicNack(deliveryTag: basicDeliveryArguments.DeliveryTag, multiple: false, requeue: true);
                }
            }
        };
#pragma warning restore VSTHRD101 // Avoid unsupported async delegates
#pragma warning restore AsyncFixer03 // Fire-and-forget async-void methods or delegates

        _channel.BasicConsume(
            queue: _queueName,
            autoAck: false,
            consumer: _consumer);

        return Task.CompletedTask;
    }
}
