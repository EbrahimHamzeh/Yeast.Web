using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class ServiceCategory
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual int? Order { get; set; }
		public virtual string Slug { get; set; }
		public virtual ICollection<Service> Services { get; set; }
	}
}
