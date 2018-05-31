using System.Web.Mvc;

namespace Yeast.Model.Admin
{
	public class EmailTemplateAdd
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}
