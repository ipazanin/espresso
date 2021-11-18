// DashboardInit.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Common.Constants;
using Espresso.Persistence.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Initialization
{
    /// <summary>
    /// Dashboard Initializer.
    /// </summary>
    public class DashboardInit : IDashboardInit
    {
        private readonly IEspressoIdentityDatabaseContext _espressoIdentityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _adminUserPassword;

        private readonly IEnumerable<string> _adminUserEmails = new[]
        {
            "ivan.pazanin1996@gmail.com",
            "miro@espressonews.co",
            "nikola.dadic@gmail.com",
            "iferencak@profico.hr",
            "pero.pavlovicit@gmail.com",
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardInit"/> class.
        /// Dashboard Initializer Constructor.
        /// </summary>
        /// <param name="espressoIdentityContext"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="adminUserPassword"></param>
        public DashboardInit(
            IEspressoIdentityDatabaseContext espressoIdentityContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            string adminUserPassword)
        {
            _espressoIdentityContext = espressoIdentityContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _adminUserPassword = adminUserPassword;
        }

        public Task InitParserDeleter()
        {
            return InitEspressoIdentityDatabase();
        }

        private async Task InitEspressoIdentityDatabase()
        {
            await _espressoIdentityContext.Database.MigrateAsync();
            if (!await _roleManager.RoleExistsAsync(roleName: RoleConstants.AdminRoleName))
            {
                var adminRole = new IdentityRole(
                    roleName: RoleConstants.AdminRoleName);
                await _roleManager.CreateAsync(role: adminRole);
            }

            foreach (var adminUserEmail in _adminUserEmails)
            {
                if (!await _userManager.Users.AnyAsync(user => user.Email == adminUserEmail))
                {
                    var adminUser = new IdentityUser
                    {
                        Email = adminUserEmail,
                        NormalizedEmail = _userManager.NormalizeEmail(adminUserEmail),
                        UserName = adminUserEmail,
                        NormalizedUserName = _userManager.NormalizeEmail(adminUserEmail),
                        EmailConfirmed = true,
                    };

                    adminUser.SecurityStamp = await _userManager.GetSecurityStampAsync(adminUser);

                    _ = await _userManager.CreateAsync(
                        user: adminUser,
                        password: _adminUserPassword);
                    await _userManager.AddToRoleAsync(adminUser, RoleConstants.AdminRoleName);
                }
            }
        }
    }
}
