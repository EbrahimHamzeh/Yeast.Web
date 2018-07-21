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
	[CustomAuthorize]
	public class EmailTemplateController : Controller
	{
		readonly IEmailTemplateService _emailTemplateService;
		readonly IUnitOfWork _uow;

		public EmailTemplateController(IUnitOfWork uow, IEmailTemplateService emailTemplateService)
		{
			_uow = uow;
            _emailTemplateService = emailTemplateService;
		}

        // GET: Admin/EmailTemplateC
        public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _emailTemplateService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

        // GET: Admin/EmailTemplateC/Add
        public virtual ActionResult Add()
		{
			EmailTemplateAdd emailTemplateAdd = new EmailTemplateAdd();
			return View(emailTemplateAdd);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        // Post: Admin/EmailTemplateC/Add/id
        public virtual ActionResult Add(EmailTemplateAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

            _emailTemplateService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

        // GET: Admin/EmailTemplateC/Edit
        public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			EmailTemplateEdit tag = _emailTemplateService.FindForEdit(id ?? 0);
			if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        // Post: Admin/EmailTemplateC/Edit/id
        public virtual ActionResult Edit(int id, EmailTemplateEdit model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _emailTemplateService.Update(model, id);
            _uow.SaveAllChanges();

            return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        // Post: Admin/EmailTemplateC/Delete/id
        public virtual ActionResult Delete(int id)
		{
			EmailTemplate emailTemplate = _emailTemplateService.Find(id);
			if (emailTemplate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _emailTemplateService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}