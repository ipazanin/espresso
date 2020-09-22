using Espresso.Application.IServices;
using Espresso.Application.Services;
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
            services.AddScoped<ICreateArticlesService, CreateArticlesService>();
            services.AddScoped<IScrapeWebService, ScrapeWebService>();
            services.AddScoped<IHttpService, HttpService>();

            return services;
        }
    }
}
