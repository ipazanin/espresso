using Espresso.Common.Enums;
using Espresso.Dashboard.Application.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Espresso.Dashboard.Startup
{
    internal sealed partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="memoryCacheInit"></param>
        public void Configure(
            IApplicationBuilder app,
            IDashboardInit memoryCacheInit
        )
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
