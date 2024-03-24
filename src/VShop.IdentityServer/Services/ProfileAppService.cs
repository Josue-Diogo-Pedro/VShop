using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace VShop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        throw new NotImplementedException();
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        throw new NotImplementedException();
    }
}
