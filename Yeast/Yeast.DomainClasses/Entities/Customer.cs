using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Customer
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Name { get; set; }
		public virtual string Family { get; set; }
		public virtual string Email { get; set; }
		public virtual string Mobile { get; set; }
		public virtual string Telephone { get; set; }
		public virtual string Description { get; set; }
	}
}
