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
	public partial class UserController : Controller
	{
		readonly IApplicationUserManager _userService;
		readonly IUnitOfWork _uow;

		public UserController(IUnitOfWork uow, IApplicationUserManager userService)
		{
			_uow = uow;
			_userService = userService;
		}

		// GET: Admin/User
		public virtual ActionResult Index()
		{
			return View();
		}

        // Get: Ajax Admin/DataList
        [HttpGet, AjaxOnly, NoOutputCache]
        public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
        {
            return Json(await _userService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/User/Add
        public virtual ActionResult Add()
		{
			User User = new User();
			return View(User);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// User: Admin/User/Add/id
		public virtual ActionResult Add(User model)
		{
			if (!ModelState.IsValid)
			
				return View(model);
			_userService.CreateAsync(model,"123456789");
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/User/Edit
		//public virtual ActionResult Edit(int? id)
		//{
		//	if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	UserEdit user = _userService.FindForEdit(id ?? 0);
		//	user.TagList = _tagService.DropDownList(user.TagIds);
		//	user.CategoryList = _categoryService.DropDownList(user.CategoryIds);
		//	if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	return View(user);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//// User: Admin/User/Edit/id
		//public virtual ActionResult Edit(int id, UserEdit userEdit)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		userEdit.TagList = _tagService.DropDownList(userEdit.TagIds);
		//		userEdit.CategoryList = _categoryService.DropDownList(userEdit.CategoryIds);
		//		return View(userEdit);
		//	}

		//	_userService.Update(userEdit, id);
		//	_uow.SaveAllChanges();
		//	return RedirectToAction("Index"); ;
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// User: Admin/User/Delete/id
		public virtual async Task<ActionResult> Delete(int id)
		{
			User user = await _userService.FindByIdAsync(id).ConfigureAwait(false);
			if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			await _userService.DeleteAsync(user);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}