using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ProfileAppService(UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        throw new NotImplementedException();
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        throw new NotImplementedException();
    }
}
