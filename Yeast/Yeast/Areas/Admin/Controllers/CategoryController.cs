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
	public partial class CategoryController : Controller
	{
		readonly ICategoryService _categoryService;
		readonly IUnitOfWork _uow;

		public CategoryController(IUnitOfWork uow, ICategoryService categoryService)
		{
			_uow = uow;
			_categoryService = categoryService;
		}

		// GET: Admin/Category
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _categoryService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Category/Add
		public virtual ActionResult Add()
		{
			CategoryAdd Category = new CategoryAdd();
			return View(Category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Add/id
		public virtual ActionResult Add(CategoryAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_categoryService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Category/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			CategoryEdit category = _categoryService.FindForEdit(id ?? 0);
			if (category == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Edit/id
		public virtual ActionResult Edit(int id)
		{
			Category categoryUpdate = _categoryService.Find(id);
			if (categoryUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(categoryUpdate, "", new string[] { "Title", "Description", "Slug", "Order" }))
			{
				if (!ModelState.IsValid)
				{
					return View(categoryUpdate);
				}
				_uow.SaveAllChanges();
			}
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Category category = _categoryService.Find(id);
			if (category == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_categoryService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}