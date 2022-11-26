// RabbitMqConfiguration.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration;

/// <summary>
///
/// </summary>
public class RabbitMqConfiguration
{
    private readonly IConfigurationSection _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqConfiguration"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    public RabbitMqConfiguration(IConfigurationSection configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Gets Host name.
    /// </summary>
    public string HostName => _configuration.GetValue<string>("HostName")!;

    /// <summary>
    /// Gets port.
    /// </summary>
    public int Port => _configuration.GetValue<int>("Port");

    /// <summary>
    /// Gets user name.
    /// </summary>
    public string Username => _configuration.GetValue<string>("Username")!;

    /// <summary>
    /// Gets password.
    /// </summary>
    public string Password => _configuration.GetValue<string>("Password")!;

    /// <summary>
    /// Gets a value indicating whether should rabbitmq server be used.
    /// </summary>
    public bool UseRabbitMqServer => _configuration.GetValue<bool>("UseRabbitMqServer");

    /// <summary>
    /// Gets articles queue name.
    /// </summary>
    public string ArticlesQueueName => _configuration.GetValue<string>("ArticlesQueueName")!;
}
