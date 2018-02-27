using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class TagEdit
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
			MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
			MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
		public string Title { get; set; }

		[MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }
	}
}
