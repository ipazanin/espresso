// Startup.Configure.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Middleware.SecurityHeaders;
using Espresso.Common.Constants;
using Espresso.Common.Enums;
using Espresso.Dashboard.Application.Initialization;
using Espresso.Domain.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Espresso.Dashboard.StartupConfiguration;

public sealed partial class Startup
{
    public void Configure(
        IApplicationBuilder app,
        ILoggerService<Startup> loggerService,
        IDashboardInit memoryCacheInit)
    {
        loggerService.Log(
            eventName: "Dashboard Startup",
            logLevel: Microsoft.Extensions.Logging.LogLevel.Information,
            namedArguments: new (string, object)[] { ("version", _dashboardConfiguration.AppConfiguration.Version) });

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
        memoryCacheInit.InitParserDeleter().GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        if (_dashboardConfiguration.AppConfiguration.AppEnvironment == AppEnvironment.Local)
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseSecurityHeadersMiddleware(securityHeadersBuilder =>
        {
            securityHeadersBuilder.AddDefaultSecurePolicy();
        });

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.StartupTag),
            });
            endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.ReadinessTag),
            });
            endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(HealthCheckConstants.LivenessTag),
            });
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
