using eBeerShop.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eBeerShop.Persistance.Utils
{
    public class SeedData
    {
        private const string _adminRoleName = "administrator";
        private const string _adminEmail = "admin@ebeershop.local";
        private const string _adminPassword = "Hello!1234";
        private string[] _defaultRoles = new string[] { _adminRoleName, "customer" };
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedData(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public static async Task Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var instance = serviceScope.ServiceProvider.GetService<SeedData>();
                await instance.Initialize();
            }
        }

        public async Task Initialize()
        {
            await EnsureRoles();
            await EnsureDefaultUser();
        }

        protected async Task EnsureRoles()
        {
            foreach (var role in _defaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        protected async Task EnsureDefaultUser()
        {
            var adminUsers = await userManager.GetUsersInRoleAsync(_adminRoleName);

            if (!adminUsers.Any())
            {
                var adminUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid(),
                    Email = _adminEmail,
                    UserName = _adminEmail
                };

                var result = await userManager.CreateAsync(adminUser, _adminPassword);
                await userManager.AddToRoleAsync(adminUser, _adminRoleName);
            }
        }
    }
}
