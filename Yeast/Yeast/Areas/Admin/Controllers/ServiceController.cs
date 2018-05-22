using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;
using System.Web;
using System.IO;
using System.Linq;
using System;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class ServiceController : Controller
	{
		readonly IServiceService _serviceService;
		readonly ITagService _tagService;
		readonly IServiceCategoryService _serviceCategoryService;
		readonly IUnitOfWork _uow;

		public ServiceController(IUnitOfWork uow, IServiceService postService, ITagService tagService, IServiceCategoryService categotyService)
		{
			_uow = uow;
			_serviceService = postService;
			_tagService = tagService;
			_serviceCategoryService = categotyService;
		}

		// GET: Admin/Service
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _serviceService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Service/Add
		public virtual ActionResult Add()
		{
			ServiceAdd Service = new ServiceAdd();
			Service.TagList = _tagService.DropDownList();
			Service.CategoryList = _serviceCategoryService.DropDownList();
			return View(Service);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Service: Admin/Service/Add/id
		public virtual ActionResult Add(ServiceAdd model)
		{
			if (!ModelState.IsValid)
			{
				model.TagList = _tagService.DropDownList(model.TagIds);
				model.CategoryList = _serviceCategoryService.DropDownList(model.CategoryIds);
				return View(model);
			}

			_serviceService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Service/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			ServiceEdit post = _serviceService.FindForEdit(id ?? 0);
			post.TagList = _tagService.DropDownList(post.TagIds);
			post.CategoryList = _serviceCategoryService.DropDownList(post.CategoryIds);
			if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(post);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Service: Admin/Service/Edit/id
		public virtual ActionResult Edit(int id, ServiceEdit postEdit)
		{
			if (!ModelState.IsValid)
			{
				postEdit.TagList = _tagService.DropDownList(postEdit.TagIds);
				postEdit.CategoryList = _serviceCategoryService.DropDownList(postEdit.CategoryIds);
				return View(postEdit);
			}

			_serviceService.Update(postEdit, id);
			_uow.SaveAllChanges();
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Service: Admin/Service/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Service post = _serviceService.Find(id);
			if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_serviceService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}

		[HttpPost]
		public virtual ActionResult FroalaUploadImage(HttpPostedFileBase file)
		{
            if (file == null)
            {
                file = Request.Files["file[]"];
            }
			var fileName = Path.GetFileName(file.FileName);
			var rootPath = Server.MapPath("~/Content/upload/images/");
			string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
			file.SaveAs(Path.Combine(rootPath, fileNameFinal));
			return Json(new { link = Url.Content("~/Content/upload/images/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public virtual ActionResult FroalaUploadFile(HttpPostedFileBase file)
		{
			var fileName = Path.GetFileName(file.FileName);
			var rootPath = Server.MapPath("~/Content/upload/files/");
			string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
			file.SaveAs(Path.Combine(rootPath, fileNameFinal));
			return Json(new { link = Url.Content("~/Content/upload/files/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public virtual ActionResult FroalaUploadVideo(HttpPostedFileBase file)
		{
			var fileName = Path.GetFileName(file.FileName);
			var rootPath = Server.MapPath("~/Content/upload/files/");
			string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
			file.SaveAs(Path.Combine(rootPath, fileNameFinal));
			return Json(new { link = Url.Content("~/Content/upload/files/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
		}
	}
}