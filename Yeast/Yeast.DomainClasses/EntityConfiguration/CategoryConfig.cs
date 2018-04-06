using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class CategoryConfig : EntityTypeConfiguration<Category>
	{
		public CategoryConfig()
		{
			Property(category => category.Title).HasMaxLength(100);
			Property(category => category.Description).HasMaxLength(1000);
			Property(category => category.Slug).HasMaxLength(100);
		}
	}
}
