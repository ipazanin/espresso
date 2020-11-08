using System.Net.Http;
using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Espresso.WebApi.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddTransient<ITrendingScoreService, TrendingScoreService>(serviceProvider => new TrendingScoreService(
                halfOfMaxTrendingScoreValue: webApiConfiguration.TrendingScoreConfiguration.HalfOfMaxTrendingScoreValue,
                ageWeight: webApiConfiguration.TrendingScoreConfiguration.AgeWeight
            ));


            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            return services;
        }
    }
}
