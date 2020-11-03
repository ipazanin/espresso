using System.Net.Http;
using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.WebApi.Configuration;
using Espresso.Wepi.Application.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServicesServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="webApiConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IWebApiConfiguration webApiConfiguration)
        {
            services.AddScoped<ISlackService, SlackService>(serviceProvider => new SlackService(
                memoryCache: serviceProvider.GetRequiredService<IMemoryCache>(),
                httpClientFactory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
                loggerService: serviceProvider.GetRequiredService<ILoggerService<SlackService>>(),
                webHookUrl: webApiConfiguration.AppConfiguration.SlackWebHook
            ));
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            return services;
        }
    }
}
