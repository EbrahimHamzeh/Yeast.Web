using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }

        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }
    }
}