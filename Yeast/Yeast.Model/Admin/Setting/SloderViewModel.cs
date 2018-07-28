using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
    public class SliderViewModel
    {
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string TextImag1 { get; set; }
        public string TextImag2 { get; set; }
        public string TextImag3 { get; set; }
        public string TextImag1En { get; set; }
        public string TextImag2En { get; set; }
        public string TextImag3En { get; set; }
        public string TextImag1Ar { get; set; }
        public string TextImag2Ar { get; set; }
        public string TextImag3Ar { get; set; }
        public string TextImag1Ru { get; set; }
        public string TextImag2Ru { get; set; }
        public string TextImag3Ru { get; set; }
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


        public string TitleFa1 { get; set; }
        [AllowHtml]
        public string BodyFa1 { get; set; }
        public string TitleEn1 { get; set; }
        [AllowHtml]
        public string BodyEn1 { get; set; }
        public string TitleSa1 { get; set; }
        [AllowHtml]
        public string BodySa1 { get; set; }
        public string TitleRu1 { get; set; }
        [AllowHtml]
        public string BodyRu1 { get; set; }
    }
}
