using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Comment
	{
		public virtual int Id { get; set; }
		public virtual string Body { get; set; }
		public virtual DateTime AddedDate { get; set; }
		public virtual bool IsApproved { get; set; }
		public virtual int LikeCount { get; set; }
		public virtual User User { get; set; }
		public virtual Post Post { get; set; }
		public virtual int? ParentId { get; set; }
		public virtual Comment Parent { get; set; }
		public ICollection<Comment> Children { get; set; }
	}
}
