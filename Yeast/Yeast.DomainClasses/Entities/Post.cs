using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public enum PageStatus
	{
		Hidden,
		Visible,
		Draft,
		Archive
	}

	public class Post
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ModifiedDate { get; set; }
		public virtual string Keyword { get; set; }
		public virtual string Description { get; set; }
		public virtual string Status { get; set; } // visible hidden draft archive
		public virtual bool? CommentStatus { get; set; }
		public virtual string Body { get; set; }
		public virtual int VisitedNumber { get; set; }
		public virtual int Like { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual User User { get; set; }
		public virtual string Slug { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
	}
}
