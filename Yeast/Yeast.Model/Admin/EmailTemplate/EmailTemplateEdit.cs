using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
	public class EmailTemplateEdit
    {
		public string Title { get; set; }
		public string Description { get; set; }
        [AllowHtml]
		public string Body { get; set; }
	}
}
