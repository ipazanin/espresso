// IdentityHostingStartup.cs
//
// © 2022 Espresso News. All rights reserved.

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Espresso.Dashboard.Areas.Identity.IdentityHostingStartup))]

namespace Espresso.Dashboard.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((_, _) =>
        {
        });
    }
}
