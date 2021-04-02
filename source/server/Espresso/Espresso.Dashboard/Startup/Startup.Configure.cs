using Espresso.Common.Enums;
using Espresso.Dashboard.Application.Initialization;
using Microsoft.AspNetCore.Builder;
using Espresso.Application.Middleware.SecurityHeaders;
using Espresso.Domain.IServices;

namespace Espresso.Dashboard.Startup
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerService"></param>
        /// <param name="memoryCacheInit"></param>
        public void Configure(
            IApplicationBuilder app,
            ILoggerService<Startup> loggerService,
            IDashboardInit memoryCacheInit
        )
        {
            loggerService.Log(
                eventName: "WebApi Startup",
                logLevel: Microsoft.Extensions.Logging.LogLevel.Information,
                namedArguments: new (string, object)[] { ("version", _dashboardConfiguration.AppConfiguration.Version) }
            );

            memoryCacheInit.InitParserDeleter().GetAwaiter().GetResult();

            if (_dashboardConfiguration.AppConfiguration.AppEnvironment == AppEnvironment.Local)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
            {
                securityHeadersBuilder.AddDefaultSecurePolicy();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHealthChecks("/health");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
