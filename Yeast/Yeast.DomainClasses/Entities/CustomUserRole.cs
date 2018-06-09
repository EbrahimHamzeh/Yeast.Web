using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
    public class CustomUserRole : IdentityUserRole<int>
    {
        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }
    }
}