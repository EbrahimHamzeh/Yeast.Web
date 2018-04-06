using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Tag
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual bool Enable { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}
}
