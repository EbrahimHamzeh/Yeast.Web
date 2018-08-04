using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{
	#region " Enumerators "
	public class PostToXML
	{
		public int PostId { get; set; }

		public string title { get; set; }

		public string link { get; set; }

		public string description { get; set; }

		public DateTime pubDate { get; set; }
	}
	#endregion

	#region " Object "
	public class XMLController : Controller
	{

        readonly IOptionService _optionService;
		readonly IProductService _productService;
		readonly IPostService _postService;
		readonly IUnitOfWork _uow;

		public XMLController(IUnitOfWork uow, IPostService postService, IProductService productService, IOptionService optionService)
		{
			_uow = uow;
			_postService = postService;
            _productService = productService;
            _optionService = optionService;
		}

		#region " Actions "
		public ContentResult RSS()
		{
			MetaTagModel mtaTagModel = new MetaTagModel();
			mtaTagModel = _optionService.GetByCulterMetaTag();

			var items = GetRssFeed();
			var rss = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("rss",
					new XAttribute("version", "2.0"),
						new XElement("channel",
							new XElement("title", string.Format("آخرین مطالب {0}", mtaTagModel.Title)),
							new XElement("link", "http://" + Request.Url.Host + "/rss"),
							new XElement("description", string.Format("آخرین مطالب {0}", mtaTagModel.Title)),
							new XElement(string.Format("copyright", "(c)" + DateTime.Now.Year + ", {0} .تمامی حقوق محفوظ است", mtaTagModel.Title)),
						from item in items
						select
						new XElement("item",
							new XElement("title", item.title),
							new XElement("description", item.description),
							new XElement("link", item.link),
							new XElement("pubDate", item.pubDate.ToUniversalTime().ToString("r"))
						)
					)
				)
			);
			return Content(rss.ToString(), "text/xml");
		}

		public ContentResult Sitemap()
		{
			XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
			var items = GetLinks();
			var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(ns + "urlset",
							from item in items
							select
							new XElement("url",
								new XElement("loc", item.link),
								new XElement("changefreq", "monthly"),
								new XElement("priority", "0.5")
								)
							)
						);
			return Content(sitemap.ToString(), "text/xml");
		}
		#endregion

		#region " Methods "
		public IEnumerable<PostToXML> GetRssFeed()
		{
			var article = _postService.GetAll();
			List<PostToXML> articleList = (from p in article
										   select new PostToXML()
										   {
											   description = p.Description,
											   link = "http://" + Request.Url.Host + "/fa/post/Details/" + p.Id,
											   pubDate = p.CreatedDate,
											   title = p.Title
										   }).ToList();
			return articleList;
		}

		public IEnumerable<PostToXML> GetLinks()
		{

			var post = _postService.GetAll();
			var product = _productService.GetAllproduct();

			List<PostToXML> productList = (from p in product
										   select new PostToXML()
										   {
												link = "http://" + Request.Url.Host + "/fa/Product/Detial/" + p.Id,
										   }).ToList();

			List<PostToXML> postList = (from p in post
									   select new PostToXML()
									   {
										   link = "http://" + Request.Url.Host + "/fa/post/Details/" + p.Id,
									   }).ToList();
			return postList.Union(productList);
		}
		#endregion
	}
	#endregion
}