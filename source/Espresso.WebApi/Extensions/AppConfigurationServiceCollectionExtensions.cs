using Espresso.WebApi.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppConfigurationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IWebApiConfiguration, WebApiConfiguration>();

            return services;
        }
    }
}
