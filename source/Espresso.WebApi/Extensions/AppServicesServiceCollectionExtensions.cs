using Espresso.Application.DomainServices;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Espresso.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppServicesServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IArticleParserService, ArticleParserService>();
            services.AddScoped<IWebScrapingService, WebScrapingService>();
            services.AddScoped<IHttpService, HttpService>();

            return services;
        }
    }
}
