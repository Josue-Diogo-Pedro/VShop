using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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

    public async void InitializeSeedUsers()
    {
        //If Admin user doesn't exist create user, define password and delegate profile
        if(await _userManager.FindByEmailAsync("admin1@com.ao") is null)
        {
            //Define data user admin
            ApplicationUser admin = new()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.ao",
                NormalizedEmail = "ADMIN1@COM.AO",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+244 922222222",
                FirstName = "Usuario",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Create admin user and delegate password
            IdentityResult resultAdmin = await _userManager.CreateAsync(admin, "Numsey@2024");
            if (resultAdmin.Succeeded)
            {
                //Include admin1 User to admin profile
                await _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin);

                //Include user claims admin
                var adminClaims = await _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                });
            }
        }
    }
}
