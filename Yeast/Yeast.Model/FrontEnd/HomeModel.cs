﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.FrontEnd
{
    public class HomeModel
    {

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string TextImag1 { get; set; }
        public string TextImag2 { get; set; }
        public string TextImag3 { get; set; }
        [AllowHtml]
        public string Bodypart1 { get; set; }
        public string Titlepart1 { get; set; }
        [AllowHtml]
        public string Bodypart2 { get; set; }
        public string Titlepart2 { get; set; }

    }
}
