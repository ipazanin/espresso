using Espresso.Application.IServices;
using Espresso.Application.Services;
using Espresso.Wepi.Application.IServices;
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
            services.AddScoped<IHttpService, HttpService>();

            return services;
        }
    }
}
