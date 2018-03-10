using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class ProductAdd {
		[Required(ErrorMessage = "عنوان محصول برای ثبت الزامی می‌باشد"),
		MaxLength(20, ErrorMessage = "عنوان محصول حداکثر ۲۰ حرف می‌باشد"),
		MinLength(3, ErrorMessage = "عنوان محصول باید بیشتر از ۳ حرف باشد")]
		public string Name { get; set; }

		[MaxLength(ErrorMessage ="حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }
	}
}
