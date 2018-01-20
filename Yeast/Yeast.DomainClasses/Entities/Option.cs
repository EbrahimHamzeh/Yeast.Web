using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Option
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Value { get; set; }
	}
}
