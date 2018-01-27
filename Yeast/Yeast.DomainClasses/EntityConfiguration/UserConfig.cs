using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class UserConfig : EntityTypeConfiguration<User>
	{
		public UserConfig()
		{
			Property(x => x.IP).HasMaxLength(20).IsOptional();
			Property(x => x.Description).HasMaxLength(1000).IsOptional();
			Property(x => x.FirstName).HasMaxLength(50).IsOptional();
			Property(x => x.LastName).HasMaxLength(50).IsOptional();
			Property(x => x.AvatarPath).HasMaxLength(200);
		}
	}
}
