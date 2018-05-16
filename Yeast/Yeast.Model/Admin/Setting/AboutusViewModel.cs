using System.Web.Mvc;

namespace Yeast.Model.Admin
{
    public class AboutusViewModel
    {
        public string TitleFa { get; set; }
        [AllowHtml]
        public string BodyFa { get; set; }
        public string TitleEn { get; set; }
        [AllowHtml]
        public string BodyEn { get; set; }
        public string TitleSa { get; set; }
        [AllowHtml]
        public string BodySa { get; set; }
        public string TitleRu { get; set; }
        [AllowHtml]
        public string BodyRu { get; set; }
    }
}
