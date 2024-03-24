namespace VShop.IdentityServer.SeedDatabase;

public interface IDatabaseSeedInitializer
{
    Task InitializeSeedRoles();
    Task InitializeSeedUsers();
}
