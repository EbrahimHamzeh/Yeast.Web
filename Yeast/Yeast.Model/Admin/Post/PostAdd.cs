﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
	public class PostAdd
	{
		[Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
		MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
		MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
		public string Title { get; set; }
        [Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
        MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
        MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
        public string TitleEn { get; set; }
        [Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
        MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
        MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
        public string TitleAr { get; set; }
        [Required(ErrorMessage = "عنوان تگ برای ثبت الزامی می‌باشد"),
        MaxLength(20, ErrorMessage = "عنوان تگ حداکثر ۲۰ حرف می‌باشد"),
        MinLength(3, ErrorMessage = "عنوان تگ باید بیشتر از ۳ حرف باشد")]
        public string TitleRu { get; set; }

        public string ImageTitle { get; set; }

        [MaxLength(ErrorMessage ="حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
		public string Description { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionEn { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionAr { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionRu { get; set; }
        [AllowHtml]
		public string Body { get; set; }
        [AllowHtml]
        public string BodyEn { get; set; }
        [AllowHtml]
        public string BodyAr { get; set; }
        [AllowHtml]
        public string BodyRu { get; set; }
        public string Slug { get; set; }
		public List<int> CategoryIds { get; set; }
		public List<int> TagIds { get; set; }
		public string Keyword { get; set; }
		public MultiSelectList TagList { get; set; }
		public MultiSelectList CategoryList { get; set; }
	}
}
