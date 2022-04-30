using GoodLawSoftware.Application;
using GoodLawSoftware.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodLawSoftware.Infrastructure
{
	public class GoodLawContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public GoodLawContext(DbContextOptions<GoodLawContext> options):base(options)
		{


		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			new UserEntityTypeConfiguration().Configure(builder.Entity<User>());
			base.OnModelCreating(builder);

		}
	}
}
