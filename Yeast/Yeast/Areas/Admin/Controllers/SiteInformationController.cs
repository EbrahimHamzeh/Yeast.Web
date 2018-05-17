using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
    public partial class SiteInformationController : Controller
	{
		//readonly ISiteInformationService _postService;
		readonly ITagService _tagService;
		readonly ICategoryService _categoryService;
		readonly IUnitOfWork _uow;

		public SiteInformationController(IUnitOfWork uow, ITagService tagService, ICategoryService categotyService)
		{
			_uow = uow;
			_tagService = tagService;
			_categoryService = categotyService;
		}

		// GET: Admin/SiteInformation
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		//[HttpGet, AjaxOnly, NoOutputCache]
		//public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		//{
		//	return Json(await _postService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		//}

		//// GET: Admin/SiteInformation/Add
		//public virtual ActionResult Add()
		//{
		//	SiteInformationAdd SiteInformation = new SiteInformationAdd();
		//	SiteInformation.TagList = _tagService.DropDownList();
		//	SiteInformation.CategoryList = _categoryService.DropDownList();
		//	return View(SiteInformation);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//// SiteInformation: Admin/SiteInformation/Add/id
		//public virtual ActionResult Add(SiteInformationAdd model)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		model.TagList = _tagService.DropDownList(model.TagIds);
		//		model.CategoryList = _categoryService.DropDownList(model.CategoryIds);
		//		return View(model);
		//	}

		//	_postService.Add(model);
		//	_uow.SaveAllChanges();
		//	return RedirectToAction("Index");
		//}

		//// GET: Admin/SiteInformation/Edit
		//public virtual ActionResult Edit(int? id)
		//{
		//	if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	SiteInformationEdit post = _postService.FindForEdit(id ?? 0);
		//	post.TagList = _tagService.DropDownList(post.TagIds);
		//	post.CategoryList = _categoryService.DropDownList(post.CategoryIds);
		//	if (post == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	return View(post);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//// SiteInformation: Admin/SiteInformation/Edit/id
		//public virtual ActionResult Edit(int id, SiteInformationEdit postEdit)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		postEdit.TagList = _tagService.DropDownList(postEdit.TagIds);
		//		postEdit.CategoryList = _categoryService.DropDownList(postEdit.CategoryIds);
		//		return View(postEdit);
		//	}

		//	_postService.Update(postEdit, id);
		//	_uow.SaveAllChanges();
		//	return RedirectToAction("Index"); ;
		//}
	}
}