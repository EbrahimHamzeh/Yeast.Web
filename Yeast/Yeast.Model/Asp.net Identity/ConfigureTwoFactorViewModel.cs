﻿using System.Collections.Generic;
using System.Web.WebPages.Html;

namespace Yeast.Model
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }

}