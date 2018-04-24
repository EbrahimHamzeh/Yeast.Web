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
	public partial class PostController : Controller
	{
		readonly IPostService _postService;
		readonly ITagService _tagService;
		readonly ICategoryService _categoryService;
		readonly IUnitOfWork _uow;

		public PostController(IUnitOfWork uow, IPostService postService, ITagService tagService, ICategoryService categotyService)
		{
			_uow = uow;
			_postService = postService;
			_tagService = tagService;
			_categoryService = categotyService;
		}

		// GET: Admin/Post
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _postService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Post/Add
		public virtual ActionResult Add()
		{
			PostAdd Post = new PostAdd();
			Post.TagList = _tagService.DropDownList();
			Post.CategoryList = _categoryService.DropDownList();
			return View(Post);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Post/Add/id
		public virtual ActionResult Add(PostAdd model)
		{
			if (!ModelState.IsValid)
			{
				model.TagList = _tagService.DropDownList(model.TagIds);
				model.CategoryList = _categoryService.DropDownList(model.CategoryIds);
				return View(model);
			}

			_postService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Post/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			PostEdit post = _postService.FindForEdit(id ?? 0);
			post.TagList = _tagService.DropDownList(post.TagIds);
			post.CategoryList = _categoryService.DropDownList(post.CategoryIds);
			if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(post);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Post/Edit/id
		public virtual ActionResult Edit(int id, PostEdit postEdit)
		{
			if (!ModelState.IsValid)
			{
				postEdit.TagList = _tagService.DropDownList(postEdit.TagIds);
				postEdit.CategoryList = _categoryService.DropDownList(postEdit.CategoryIds);
				return View(postEdit);
			}

			_postService.Update(postEdit, id);
			_uow.SaveAllChanges();
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Post/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Post post = _postService.Find(id);
			if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_postService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}

		[HttpPost, AjaxOnly]
		public virtual ActionResult FroalaUploadImage(HttpPostedFileBase file)
		{
			var fileName = Path.GetFileName(file.FileName);
			var rootPath = Server.MapPath("~/Content/upload/images/");
			string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
			file.SaveAs(Path.Combine(rootPath, fileNameFinal));
			return Json(new { link = Url.Content("~/Content/upload/images/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, AjaxOnly]
		public virtual ActionResult FroalaUploadFile(HttpPostedFileBase file)
		{
			var fileName = Path.GetFileName(file.FileName);
			var rootPath = Server.MapPath("~/Content/upload/files/");
			string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
			file.SaveAs(Path.Combine(rootPath, fileNameFinal));
			return Json(new { link = Url.Content("~/Content/upload/files/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, AjaxOnly]
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