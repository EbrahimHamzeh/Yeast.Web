using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class ProductConfig : EntityTypeConfiguration<Product>
	{
		public ProductConfig()
		{
			Property(x => x.Name).HasMaxLength(200);
			Property(x => x.Description).HasMaxLength(4000);
		}
	}
}
