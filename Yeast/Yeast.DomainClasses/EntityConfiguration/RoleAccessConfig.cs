using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class RoleAccessConfig : EntityTypeConfiguration<RoleAccess>
	{
		public RoleAccessConfig()
		{
            //Property(ra => ra.Action).IsUnicode(false).HasMaxLength(70).IsRequired();
            //Property(ra => ra.Controller).IsUnicode(false).HasMaxLength(70).IsRequired();
            //HasRequired(ra => ra.Role).WithMany(r => r.RoleAccesses).HasForeignKey(ra => ra.RoleId);
        }
	}
}
