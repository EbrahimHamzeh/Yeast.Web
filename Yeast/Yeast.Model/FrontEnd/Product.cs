using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Yeast.Model.FrontEnd
{
	public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
        [AllowHtml]
        public virtual string Body { get; set; }
        //public string NameEn { get; set; }
        //[MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        //public string DescriptionEn { get; set; }
        //[AllowHtml]
        //public string BodyEn { get; set; }
        //public string NameAr { get; set; }
        //[MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        //public string DescriptionAr { get; set; }
        //[AllowHtml]
        //public string BodyAr { get; set; }
        //public string NameRu { get; set; }
        //[MaxLength(ErrorMessage = "حداکثر ۱۰۰۰ کاراکتر می‌توانید وارد کنید")]
        //public string DescriptionRu { get; set; }
        //[AllowHtml]
        //public string BodyRu { get; set; }
        public decimal Price { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string MetaData { get; set; }
    }
}
