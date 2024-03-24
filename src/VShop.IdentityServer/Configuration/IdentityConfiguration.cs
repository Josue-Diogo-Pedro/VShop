using Duende.IdentityServer;
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

    public static IEnumerable<Client> Clients => new List<Client>
    {
        //Generic client
        new Client
        {
            ClientId = "client",
            ClientSecrets = {new Secret("abracadabra#simsalabim".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials, //needs user credentials
            AllowedScopes = {"read", "write", "profile"}
        },
        new Client
        {
            ClientId = "vshop",
            ClientSecrets = {new Secret("abracadabra#simsalabim".Sha256())},
            AllowedGrantTypes = GrantTypes.Code, //with code
            RedirectUris = { "https://localhost:7295/signin-oidc" }, //login
            PostLogoutRedirectUris = { "https://localhost:7295/signout-callback-oidc" }, //logout,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "vshop"
            }
        }
    };
}
