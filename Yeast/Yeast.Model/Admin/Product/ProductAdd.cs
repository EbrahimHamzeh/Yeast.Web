using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
    public class ProductAdd
    {
        [Required(ErrorMessage = "عنوان محصول برای ثبت الزامی می‌باشد"),
        MaxLength(20, ErrorMessage = "عنوان محصول حداکثر ۲۰ حرف می‌باشد"),
        MinLength(3, ErrorMessage = "عنوان محصول باید بیشتر از ۳ حرف باشد")]
        public string Name { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string Description { get; set; }
        [AllowHtml]
        public virtual string Body { get; set; }
        public string NameEn { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionEn { get; set; }
        [AllowHtml]
        public string BodyEn { get; set; }
        public string NameAr { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionAr { get; set; }
        [AllowHtml]
        public string BodyAr { get; set; }
        public string NameRu { get; set; }
        [MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        public string DescriptionRu { get; set; }
        [AllowHtml]
        public string BodyRu { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public string MetaData { get; set; }
    }
}
