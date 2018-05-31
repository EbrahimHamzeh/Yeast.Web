using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class EmailTemplate
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Body { get; set; }
		public virtual bool Enable { get; set; }
		public virtual string Description { get; set; }
	}
}
