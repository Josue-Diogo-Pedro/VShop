using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        //vshop is web application that will access IdentityServer to obtain the token
        new ApiScope("vshop", "VShop Server"),
        new ApiScope("read", "Read data"),
        new ApiScope("write", "Write data"),
        new ApiScope("delete", "Delete data")
    };
}
