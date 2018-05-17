using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class ContactUs
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Mobile { get; set; }
		public virtual string Email { get; set; }
		public virtual string Message { get; set; }
	}
}
