using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class PostController : Controller
	{
		// GET: Admin/Post
		public virtual ActionResult Index()
		{
			return View();
		}

		// GET: Admin/Post/Add
		public virtual ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Add(int? id)
		{
			return View();
		}

		// GET: Admin/Post/Edit
		public virtual ActionResult Edit()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(int? id)
		{
			return View();
		}
	}
}