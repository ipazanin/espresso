using Espresso.Application.DomainServices;
using Espresso.Domain.IServices;
using Espresso.Domain.Services;
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
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IArticleParserService, ArticleParserService>();
            services.AddScoped<IWebScrapingService, WebScrapingService>();
            services.AddScoped<IHttpService, HttpService>();

            return services;
        }
    }
}
