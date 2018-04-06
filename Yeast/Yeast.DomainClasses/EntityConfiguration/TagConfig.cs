using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class TagConfig : EntityTypeConfiguration<Tag>
	{
		public TagConfig()
		{
			Property(x => x.Title).HasMaxLength(200);
			Property(x => x.Description).HasMaxLength(1000);
		}
	}
}
