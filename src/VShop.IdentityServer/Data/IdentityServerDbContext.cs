using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VShop.IdentityServer.Data;

public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>
{
	public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options) { }
}
