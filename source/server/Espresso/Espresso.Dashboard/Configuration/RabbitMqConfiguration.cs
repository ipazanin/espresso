using Microsoft.Extensions.Configuration;

namespace Espresso.Dashboard.Configuration
{
    public class RabbitMqConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        public string HostName => _configuration.GetValue<string>("HostName");

        public int Port => _configuration.GetValue<int>("Port");

        public string Username => _configuration.GetValue<string>("Username");

        public string Password => _configuration.GetValue<string>("Password");

        public bool UseRabbitMqServer => _configuration.GetValue<bool>("UseRabbitMqServer");

        public string ArticlesQueueName => _configuration.GetValue<string>("ArticlesQueueName");
        #endregion

        #region Constructors
        public RabbitMqConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
