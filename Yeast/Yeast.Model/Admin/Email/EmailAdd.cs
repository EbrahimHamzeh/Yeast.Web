using System.Collections.Generic;
using System.Web.Mvc;

namespace Yeast.Model.Admin
{
	public class EmailAdd
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Body { get; set; }
		public List<int> EmailTemplateIds { get; set; }
		public List<int> UserIds { get; set; }
        public MultiSelectList EmailTemplateList { get; set; }
        public MultiSelectList UserList { get; set; }
    }
}
