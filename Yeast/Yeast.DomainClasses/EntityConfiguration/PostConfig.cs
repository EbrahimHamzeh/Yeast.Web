using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class PostConfig : EntityTypeConfiguration<Post>
	{
		public PostConfig()
		{
			Property(x => x.Body).IsMaxLength().IsOptional();
			Property(x => x.Description).HasMaxLength(400).IsOptional();
			Property(x => x.Keyword).HasMaxLength(500).IsOptional();
			Property(x => x.Title).HasMaxLength(200);
		}
	}
}
