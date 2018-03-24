using System.Web.Mvc;

namespace Yeast.DomainClasses.Entities
{
	public class PostTranslate
	{
		public virtual int Id { get; set; }

		public virtual string Title { get; set; }

		public virtual string Keyword { get; set; }

		[AllowHtml]
		public virtual string Body { get; set; }

		public virtual string LanguageCode { get; set; }

		public virtual Post Post { get; set; }

	}
}
