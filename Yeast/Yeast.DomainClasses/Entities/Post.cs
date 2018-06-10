using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? ModifiedDate { get; set; }
		public virtual string Title { get; set; }
        public virtual string TitleEn { get; set; }
        public virtual string TitleAr { get; set; }
        public virtual string TitleRu { get; set; }
        public virtual string ImageTitle { get; set; }
        public virtual string Keyword { get; set; }
		[AllowHtml]
		public virtual string Body { get; set; }
        [AllowHtml]
        public virtual string BodyEn { get; set; }
        [AllowHtml]
        public virtual string BodyAr { get; set; }
        [AllowHtml]
        public virtual string BodyRu { get; set; }
        public virtual string Slug { get; set; }
		public virtual string Description { get; set; }
        public virtual string DescriptionEn { get; set; }
        public virtual string DescriptionAr { get; set; }
        public virtual string DescriptionRu { get; set; }
        //public virtual User User { get; set; }   TODO: Hamzeh?????????????
        public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
		public virtual ICollection<Category> Categories { get; set; }

	}
}
