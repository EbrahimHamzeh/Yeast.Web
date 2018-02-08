using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class PostTranslate
	{
		public virtual int Id { get; set; }

		public virtual string Title { get; set; }

		public virtual string Keyword { get; set; }

		public virtual string Description { get; set; }

		public virtual string Body { get; set; }

		public virtual string LanguageCode { get; set; }

		public virtual ICollection<Post> Posts { get; set; }

	}
}
