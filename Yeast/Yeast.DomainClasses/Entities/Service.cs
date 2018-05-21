using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Yeast.DomainClasses.Entities
{
	public class Service
    {
		public virtual int Id { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ModifiedDate { get; set; }
		public virtual string Title { get; set; }
		public virtual string Keyword { get; set; }
		[AllowHtml]
		public virtual string Body { get; set; }
		public virtual string Slug { get; set; }
		public virtual string Description { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
		public virtual ICollection<ServiceCategory> ServiceCategories { get; set; }

	}
}
