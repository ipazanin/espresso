// RabbitMqConfiguration.cs
//
// © 2021 Espresso News. All rights reserved.

using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class RabbitMqConfiguration
    {
        private readonly IConfigurationSection _configuration;

        public string HostName => _configuration.GetValue<string>("HostName");

        public int Port => _configuration.GetValue<int>("Port");

        public string Username => _configuration.GetValue<string>("Username");

        public string Password => _configuration.GetValue<string>("Password");

        public bool UseRabbitMqServer => _configuration.GetValue<bool>("UseRabbitMqServer");

        public string ArticlesQueueName => _configuration.GetValue<string>("ArticlesQueueName");

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqConfiguration"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public RabbitMqConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
    }
}
