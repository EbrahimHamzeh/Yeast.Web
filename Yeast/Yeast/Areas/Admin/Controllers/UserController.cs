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
using System.Collections.Generic;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class UserController : Controller
	{
        readonly IApplicationRoleManager _roleService;
        readonly IApplicationUserManager _userService;
		readonly IUnitOfWork _uow;

		public UserController(IUnitOfWork uow, IApplicationUserManager userService, IApplicationRoleManager roleService)
		{
			_uow = uow;
			_userService = userService;
            _roleService = roleService;
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
            ViewBag.RoleList = _roleService.GetAllCustomRolesAsync().Result;
            return View(User);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// User: Admin/User/Add/id
		public virtual ActionResult Add(User model)
		{

            if (!ModelState.IsValid)
				return View(model);

            HttpPostedFileBase file = Request.Files["user-avatar"];
            model.AvatarPath = file.FileName;
            model.Email = model.Email;
            model.UserName = model.Email;
            model.CreatedDate = DateTime.Now;
            model.BirthDay = DateTime.Now;
            model.IP = Request.UserHostAddress;
            model.IsBaned = false;

            var path = Path.Combine(Server.MapPath("~/Content/upload/avatar/"), file.FileName);
            file.SaveAs(path);
            var userCreate = _userService.CreateAsync(model,model.Password);

            if (userCreate.Result.Errors.Count() == 0)
            {
                string[] roleIds = Request["CategoryIds"].Split(',');
                foreach (var item in roleIds)
                {
                    _userService.AddToRoleAsync(userCreate.Id, _roleService.FindByIdAsync(Convert.ToInt32(item)).Result.Name);
                }
            }

            foreach (var item in userCreate.Result.Errors)
            {
                        ModelState.AddModelError(Guid.NewGuid().ToString(),item.ToString());
            }
            if (!ModelState.IsValid)
                return View(model);
            _uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

        //GET: Admin/User/Edit
        public virtual ActionResult Edit(int? id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = _userService.FindById(id ?? 0);
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // User: Admin/User/Edit/id
        public virtual ActionResult Edit(int id, User userEdit)
        {
            HttpPostedFileBase file = Request.Files["user-avatar"];
            userEdit.BirthDay = DateTime.Now;
            if(file.FileName!="")
            {
                if (!ModelState.IsValid)
                {
                    return View(userEdit);
                }
                var path = Path.Combine(Server.MapPath("~/Content/upload/avatar/"), file.FileName);
                file.SaveAs(path);
                userEdit.AvatarPath = file.FileName;
                userEdit.UserName = userEdit.Email;
                _userService.UpdateAsync(userEdit);
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(userEdit);
                }
                userEdit.UserName = userEdit.Email;
                _userService.UpdateAsync(userEdit);
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }
           
        }

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