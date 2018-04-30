using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
	public class ProductAdd {
		[Required(ErrorMessage = "عنوان محصول برای ثبت الزامی می‌باشد"),
		MaxLength(20, ErrorMessage = "عنوان محصول حداکثر ۲۰ حرف می‌باشد"),
		MinLength(3, ErrorMessage = "عنوان محصول باید بیشتر از ۳ حرف باشد")]
		public string Name { get; set; }

		[MaxLength(ErrorMessage ="حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }

        [AllowHtml]
        public virtual string Body { get; set; }
        public decimal Price { get; set; }
        public List<ImageJSON> Image1 { get; set; }
        public string MetaData { get; set; }
    }
    public class ImageJSON
    {
        public string link { get; set; }
    }
}
