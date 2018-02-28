using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
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

		public virtual ActionResult DataList()
		{
			var temp = new { rows = _tagService.GetAll(), total = _tagService.Count };
			return Json(temp, JsonRequestBehavior.AllowGet);
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
			_tagService.Add(model);
			_uow.SaveAllChanges();	
			return RedirectToAction("Index");
		}

		// GET: Admin/Tag/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			Tag tag = _tagService.Find(id??0);
			if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(int id)
		{
			Tag tagUpdate = _tagService.Find(id);
			if (tagUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(tagUpdate, "", new string[] { "Title", "Description" }))
			{
				if(!ModelState.IsValid)
				{
					return View(tagUpdate);
				}
				_uow.SaveAllChanges();
			}
				return View();
		}
	}
}