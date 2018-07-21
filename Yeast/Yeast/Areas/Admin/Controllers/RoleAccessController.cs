using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;
using System.Collections.Generic;
using System;
using Yeast.Utilities.DynamicRoleBase;
using Yeast.Servicelayer.EFServices;

namespace Yeast.Areas.Admin.Controllers
{
	[CustomAuthorize]
	public class RoleAccessController : Controller
	{
        readonly IApplicationRoleManager _roleService;
        readonly IUnitOfWork _uow;
        private static Lazy<IEnumerable<ControllerDescription>> _controllerList = new Lazy<IEnumerable<ControllerDescription>>();

        public RoleAccessController(IUnitOfWork uow, IApplicationRoleManager roleService)
		{
            _uow = uow;
            _roleService = roleService;
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
            return Json(await _roleService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Tag/Add
        public virtual ActionResult Add()
		{
            var createRoleViewModel = new RoleAccessAdd
            {
                Controllers = GetControllers()
            };
            return View(createRoleViewModel);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Add/id
		public virtual ActionResult Add(RoleAccessAdd model)
		{
            if (!ModelState.IsValid)
            {
                model.Controllers = GetControllers();
                return View(model);
            }

            var role = new CustomRole
            {
                Title = model.Title,
                Name = Guid.NewGuid().ToString("N"),
                RoleAccesses = new List<RoleAccess>(),
                Description = model.Description,
            };

            if (!string.IsNullOrEmpty(Request["RoleTotal"]))
            {
                var selected = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActionControllers>>(Request["RoleTotal"]);
                foreach (var item in selected)
                {
                    if (item.Controller == "#")
                    {
                    }
                    else
                    {
                        role.RoleAccesses.Add(new RoleAccess { Controller = item.Controller, Action = item.Action });
                    }
                }
                _roleService.CreateAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                model.Controllers = GetControllers();
                return View(model);
            }
        }

        // GET: Admin/Tag/Edit
        public virtual ActionResult Edit(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CustomRole customRole = _roleService.FindByIdAsync(id).Result;
            if (customRole == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            RoleAccessEdit roleAccessEdit = new RoleAccessEdit()
            {
                Controllers = GetControllers(),
                Description = customRole.Description,
                Title = customRole.Title
            };
            return View(roleAccessEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Post: Admin/Tag/Edit/id
        public virtual ActionResult Edit(int id, RoleAccessEdit model)
        {
            if (!ModelState.IsValid)
            {
                model.Controllers = GetControllers();
                return View(model);
            }

            var role = _roleService.FindByIdAsync(id).Result;

            if (!string.IsNullOrEmpty(Request["RoleTotal"]))
            {
                var selected = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActionControllers>>(Request["RoleTotal"]);
                foreach (var item in selected)
                {
                    if (item.Controller == "#")
                    {
                    }
                    else
                    {
                        role.RoleAccesses.Add(new RoleAccess { Controller = item.Controller, Action = item.Action });
                    }
                }
                _roleService.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                model.Controllers = GetControllers();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Post: Admin/Tag/Delete/id
        public virtual ActionResult Delete(int id)
        {
            CustomRole tag = _roleService.FindByIdAsync(id).Result;
            if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _roleService.DeleteAsync(tag);
            _uow.SaveAllChanges();
            return Json(new { success = true });
        }

        private static IEnumerable<ControllerDescription> GetControllers()
        {
            if (_controllerList.IsValueCreated)
                return _controllerList.Value;

            _controllerList = new Lazy<IEnumerable<ControllerDescription>>(() => new ControllerHelper().GetControllersNameAnDescription("Yeast.Controllers")); // تمام کنترلرها به فیر از همانی که داخل پرانتز نوشتیم
            return _controllerList.Value;
        }
    }
}