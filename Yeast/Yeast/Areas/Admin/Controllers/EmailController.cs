using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;
using System;

namespace Yeast.Areas.Admin.Controllers
{
	[CustomAuthorize]
	public class EmailController : Controller
	{
		readonly IEmailService _emailService;
		readonly IEmailTemplateService _emailTemplateService;
		readonly IUnitOfWork _uow;
		readonly IApplicationUserManager _userService;

		public EmailController(IUnitOfWork uow, IEmailService emailService, IEmailTemplateService emailTemplateService, IApplicationUserManager userService)
		{
			_uow = uow;
			_emailService = emailService;
			_emailTemplateService = emailTemplateService;
			_userService = userService;

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
			return Json(await _emailService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Tag/Add
		public virtual ActionResult Add()
		{
			EmailAdd emailAdd = new EmailAdd();
			emailAdd.EmailTemplateList = _emailTemplateService.DropDownList();
			emailAdd.UserList = _userService.DropDownList();
			return View(emailAdd);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Add/id
		public virtual ActionResult Add(EmailAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			string[] emails;
			bool result;
			if (!string.IsNullOrEmpty(Request["Emails[]"]))
			{
				emails = Request["Emails[]"].Split(',');
				foreach (string email in emails)
				{
					result = Utilities.Email.MailSender.SendMail(model.Title, model.Body, email);
					if (result) _emailService.Add(new Email { Title = model.Title, Body = model.Body, SendTime = DateTime.Now, EmailAddress = email});
				}
			}

			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Email tag = _emailService.Find(id);
			if (tag == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_emailService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}