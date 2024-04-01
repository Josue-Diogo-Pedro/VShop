using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfileAppService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _roleManager = roleManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //user id in IS
        var id = context.Subject.GetSubjectId();

        //Find user by id
        ApplicationUser user = await _userManager.FindByIdAsync(id);

        //Create principal claims to user
        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        //Define collection of claims to user and include surname and user name
        List<Claim> claims = userClaims.Claims.ToList();
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

        //Se o userManager do identity suportar role
        if (_userManager.SupportsUserRole)
        {
            //Obtain list of role names to user
            IList<string> roles = await _userManager.GetRolesAsync(user);

            //navigate to list
            foreach (var role in roles)
            {
                //add role into claim
                claims.Add(new Claim(JwtClaimTypes.Role, role));

                //Se _rolemanager suportar claims para roles
                if (_roleManager.SupportsRoleClaims)
                {
                    //find user
                    IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                    //include profile
                    if(identityRole is not null)
                    {
                        //include the claims associate by role
                        claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }

        }

        //return the claims of context
        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        //Obtain user id in IS
        var id = context.Subject.GetSubjectId();

        //Localiza o usuario
        ApplicationUser user = await _userManager.FindByIdAsync(id);

        context.IsActive = user is not null;
    }
}
