using System.Collections.Generic;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Espresso.Dashboard.Application.Initialization
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardInit : IDashboardInit
    {
        #region Fields

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
            "pero.pavlovicit@gmail.com"
        };

        #endregion
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="context"></param>
        /// <param name="memoryCache"></param>
        /// <param name="loggerFactory"></param>
        public DashboardInit(
            IEspressoIdentityDatabaseContext espressoIdentityContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            string adminUserPassword
        )
        {
            _espressoIdentityContext = espressoIdentityContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _adminUserPassword = adminUserPassword;
        }
        #endregion

        #region Methods
        public async Task InitParserDeleter()
        {
            await InitEspressoIdentityDatabase();
        }

        private async Task InitEspressoIdentityDatabase()
        {
            await _espressoIdentityContext.Database.MigrateAsync();
            if (!await _roleManager.RoleExistsAsync(roleName: RoleConstants.AdminRoleName))
            {
                var adminRole = new IdentityRole(
                    roleName: RoleConstants.AdminRoleName
                );
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
                        EmailConfirmed = true
                    };

                    adminUser.SecurityStamp = await _userManager.GetSecurityStampAsync(adminUser);

                    var identityResult = await _userManager.CreateAsync(
                        user: adminUser,
                        password: _adminUserPassword
                    );
                    await _userManager.AddToRoleAsync(adminUser, RoleConstants.AdminRoleName);
                }
            }
        }
        #endregion
    }
}
