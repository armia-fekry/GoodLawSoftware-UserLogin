using GoodLawSoftware.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodLawSoftware.Infrastructure.Configuration
{
	public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");
			builder.Property(u => u.UserName).IsRequired();
			builder.Property(u => u.Password).IsRequired();
			builder.Property(u => u.Email).IsRequired();

		}
	}
}
