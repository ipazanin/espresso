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
            services.AddScoped<ISlackService, SlackService>(o => new SlackService(
                memoryCache: o.GetRequiredService<IMemoryCache>(),
                httpClientFactory: o.GetRequiredService<IHttpClientFactory>(),
                loggerFactory: o.GetRequiredService<ILoggerFactory>(),
                webHookUrl: webApiConfiguration.AppConfiguration.SlackWebHook
            ));
            services.AddScoped<IHttpService, HttpService>();

            return services;
        }
    }
}
