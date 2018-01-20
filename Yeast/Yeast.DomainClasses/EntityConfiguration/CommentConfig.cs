using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class CommentConfig : EntityTypeConfiguration<Comment>
	{
		public CommentConfig()
		{
			HasOptional(x => x.Parent)
					.WithMany(x => x.Children)
					.HasForeignKey(x => x.ParentId)
					.WillCascadeOnDelete(false);
			HasOptional(x => x.Post).WithMany(x => x.Comments).WillCascadeOnDelete();

			Property(x => x.Body).HasMaxLength(4000);
		}
	}
}
