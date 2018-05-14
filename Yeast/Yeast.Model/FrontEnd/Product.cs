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
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string MetaData { get; set; }
    }
}
