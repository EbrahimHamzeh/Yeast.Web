using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class TagController : Controller
	{
		readonly ITagService _tagService;
		readonly IUnitOfWork _uow;
		public TagController(IUnitOfWork uow, ITagService tagService)
		{
			_uow = uow;
			_tagService = tagService;
		}
		// GET: Admin/Tag
		public virtual ActionResult Index()
		{
			return View();
		}

		// GET: Admin/Tag/Add
		public virtual ActionResult Add()
		{
			TagAdd Tag = new TagAdd();
			return View(Tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Add(TagAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				_tagService.Add(model);
				_uow.SaveAllChanges();
			}
			catch (Exception)
			{

			}
			
			return View();
		}

		// GET: Admin/Tag/Edit
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