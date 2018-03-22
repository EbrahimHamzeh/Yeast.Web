using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;

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

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _tagService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Tag/Add
		public virtual ActionResult Add()
		{
			TagAdd Tag = new TagAdd();
			return View(Tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Add/id
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
			TagEdit tag = _tagService.FindForEdit(id ?? 0);
			if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Edit/id
		public virtual ActionResult Edit(int id)
		{
			Tag tagUpdate = _tagService.Find(id);
			if (tagUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(tagUpdate, "", new string[] { "Name", "Description" }))
			{
				if (!ModelState.IsValid)
				{
					return View(tagUpdate);
				}
				_uow.SaveAllChanges();
			}
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Tag tag = _tagService.Find(id);
			if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_tagService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}