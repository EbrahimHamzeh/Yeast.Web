using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Yeast.Utilities;

namespace Yeast.Model.Admin
{
	public class ProductEdit {
		[Required(ErrorMessage = "عنوان محصول برای ثبت الزامی می‌باشد"),
			MaxLength(20, ErrorMessage = "عنوان محصول حداکثر ۲۰ حرف می‌باشد"),
			MinLength(3, ErrorMessage = "عنوان محصول باید بیشتر از ۳ حرف باشد")]
		public string Name { get; set; }

		[MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }

        [AllowHtml]
        public virtual string Body { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public string ImageNameDeleted { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string MetaData { get; set; }
        public List<ImageJSON> ImageJSONs { get; set; }
    }
}
