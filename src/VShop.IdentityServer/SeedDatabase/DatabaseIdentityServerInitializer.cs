using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void InitializeSeedRoles()
    {
        //If Admin profile does not exist, then create profile
        if(!await _roleManager.RoleExistsAsync(IdentityConfiguration.Admin))
        {
            //Create Admin profile
            IdentityRole roleAdmin = new();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            await _roleManager.CreateAsync(roleAdmin);
        }

        //If Client profile doesn't exist, then create profile
        if(!await _roleManager.RoleExistsAsync(IdentityConfiguration.Client))
        {
            IdentityRole roleClient = new();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            await _roleManager.CreateAsync(roleClient);
        }
    }

    public void InitializeSeedUsers()
    {
        throw new NotImplementedException();
    }
}
