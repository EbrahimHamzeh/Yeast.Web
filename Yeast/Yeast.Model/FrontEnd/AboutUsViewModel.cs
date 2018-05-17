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
	public class AboutUsViewModel
    {
		public string Title { get; set; }
        [AllowHtml]
        public virtual string Body { get; set; }
    }
}
