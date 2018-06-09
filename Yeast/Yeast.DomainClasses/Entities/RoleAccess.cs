using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class RoleAccess
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int RoleId { get; set; }

        public virtual CustomUserRole Role { get; set; }
    }
}
