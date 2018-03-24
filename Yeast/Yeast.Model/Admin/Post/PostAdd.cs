using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yeast.Model.Admin
{
	public class PostAdd
	{
		[Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
		MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
		MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
		public string Title { get; set; }

		[MaxLength(ErrorMessage ="حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }
		public string Body { get; set; }
		public string Slug { get; set; }
		public string Tags { get; set; }
		public string Keyword { get; set; }
		public List<TagList> TagList { get; set; }
	}
}
