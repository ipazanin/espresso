using Microsoft.Extensions.Configuration;

namespace Espresso.WebApi.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMqConfiguration
    {
        #region Fields
        private readonly IConfigurationSection _configuration;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string HostName => _configuration.GetValue<string>("HostName");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Port => _configuration.GetValue<int>("Port");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Username => _configuration.GetValue<string>("Username");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Password => _configuration.GetValue<string>("Password");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UseRabbitMqServer => _configuration.GetValue<bool>("UseRabbitMqServer");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ArticlesQueueName => _configuration.GetValue<string>("ArticlesQueueName");
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public RabbitMqConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
