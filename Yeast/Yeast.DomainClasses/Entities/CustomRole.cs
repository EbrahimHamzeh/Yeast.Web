using Microsoft.AspNet.Identity.EntityFramework;

namespace Yeast.DomainClasses.Entities
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}