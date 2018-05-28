using System.Web.Mvc;

namespace Yeast.DomainClasses.Entities
{
	public class Product {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
        [AllowHtml]
        public virtual string Body { get; set; }
        public decimal Price { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string MetaData { get; set; }
    }
}
